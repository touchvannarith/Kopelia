﻿using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text.RegularExpressions;
using iTextSharp.text.html;

namespace NotaliaOnline.Helpers
{

    #region HtmlToPdfBuilder Class

    /// <summary>
    /// Simplifies generating HTML into a PDF file -http://aspnettutorialonline.blogspot.com/
    /// </summary>
    public class HtmlToPdfBuilder
    {

        #region Constants

        private const string STYLE_DEFAULT_TYPE = "style";
        private const string DOCUMENT_HTML_START = "<html><body>";
        private const string DOCUMENT_HTML_END = "</body></html>";
        private const string REGEX_GROUP_SELECTOR = "selector";
        private const string REGEX_GROUP_STYLE = "style";

        //amazing regular expression magic
        private const string REGEX_GET_STYLES = @"(?<selector>[^\{\s]+\w+(\s\[^\{\s]+)?)\s?\{(?<style>[^\}]*)\}";

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new PDF document template. Use PageSizes.{DocumentSize}
        /// </summary>
        public HtmlToPdfBuilder(Rectangle size)
        {
            PageSize = size;
            _Pages = new List<HtmlPdfPage>();
            _Styles = new StyleSheet();
        }

        #endregion

        #region Delegates

        /// <summary>
        /// Method to override to have additional control over the document
        /// </summary>
        public event RenderEvent BeforeRender;

        /// <summary>
        /// Method to override to have additional control over the document
        /// </summary>
        public event RenderEvent AfterRender;

        #endregion

        #region Properties

        /// <summary>
        /// The page size to make this document
        /// </summary>
        public Rectangle PageSize { get; set; }

        /// <summary>
        /// Returns the page at the specified index
        /// </summary>
        public HtmlPdfPage this[int index]
        {
            get
            {
                return _Pages[index];
            }
        }

        /// <summary>
        /// Returns a list of the pages available
        /// </summary>
        public HtmlPdfPage[] Pages
        {
            get
            {
                return this._Pages.ToArray(); //http://aspnettutorialonline.blogspot.com/
            }
        }

        #endregion

        #region Members

        private List<HtmlPdfPage> _Pages;
        private StyleSheet _Styles;

        #endregion

        #region Working With The Document

        /// <summary>
        /// Appends and returns a new page for this document http://aspnettutorialonline.blogspot.com/
        /// </summary>
        public HtmlPdfPage AddPage()
        {
            HtmlPdfPage page = new HtmlPdfPage();
            _Pages.Add(page);
            return page;
        }

        /// <summary>
        /// Removes the page from the document http://aspnettutorialonline.blogspot.com/
        /// </summary>
        public void RemovePage(HtmlPdfPage page)
        {
            _Pages.Remove(page);
        }

        /// <summary>
        /// Appends a style for this sheet http://aspnettutorialonline.blogspot.com/
        /// </summary>
        public void AddStyle(string selector, string styles)
        {
            _Styles.LoadTagStyle(selector, STYLE_DEFAULT_TYPE, styles);
        }

        /// <summary>
        /// Imports a stylesheet into the document
        /// </summary>
        public void ImportStylesheet(string path)
        {
            //load the file
            string content = File.ReadAllText(path);

            //use a little regular expression magic
            foreach (Match match in Regex.Matches(content, REGEX_GET_STYLES))
            {
                string selector = match.Groups[REGEX_GROUP_SELECTOR].Value;
                string style = match.Groups[REGEX_GROUP_STYLE].Value;
                AddStyle(selector, style);
            }
        }


        #endregion

        #region Document Navigation

        /// <summary>
        /// Moves a page before another
        /// </summary>
        public void InsertBefore(HtmlPdfPage page, HtmlPdfPage before)
        {
            _Pages.Remove(page);
            _Pages.Insert(
                Math.Max(_Pages.IndexOf(before), 0),
                page);
        }

        /// <summary>
        /// Moves a page after another
        /// </summary>
        public void InsertAfter(HtmlPdfPage page, HtmlPdfPage after)
        {
            _Pages.Remove(page);
            _Pages.Insert(
                Math.Min(_Pages.IndexOf(after) + 1, _Pages.Count),
                page);
        }


        #endregion

        #region Rendering The Document

        /// <summary>
        /// Renders the PDF to an array of bytes
        /// </summary>
        public byte[] RenderPdf()
        {
            //_Styles.LoadTagStyle("img", "margin-top", "-50px");

            //Document is inbuilt class, available in iTextSharp
            MemoryStream file = new MemoryStream();
            Document document = new Document(PageSize, 30f, 30f, 30f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(document, file);

            //allow modifications of the document
            if (BeforeRender is RenderEvent)
            {
                BeforeRender(writer, document);
            }

            //header
            document.Add(new Header(Markup.HTML_ATTR_STYLESHEET, string.Empty));
            document.Open();

            //render each page that has been added
            foreach (HtmlPdfPage page in _Pages)
            {
                document.NewPage();

                //generate this page of text
                MemoryStream output = new MemoryStream();
                StreamWriter html = new StreamWriter(output, Encoding.UTF8);

                //get the page output
                html.Write(string.Concat(DOCUMENT_HTML_START, page._Html.ToString(), DOCUMENT_HTML_END));
                html.Close();
                html.Dispose();

                //read the created stream
                MemoryStream generate = new MemoryStream(output.ToArray());
                StreamReader reader = new StreamReader(generate);
                foreach (object item in HTMLWorker.ParseToList(reader, _Styles))
                {
                    document.Add((IElement)item);
                }
                
                //cleanup these streams
                html.Dispose();
                reader.Dispose();
                output.Dispose();
                generate.Dispose();
            }

            //after rendering
            if (AfterRender is RenderEvent)
            {
                AfterRender(writer, document);
            }

            //return the rendered PDF
            document.Close();
            return file.ToArray();

        }

        #endregion

    }

    #endregion

    #region HtmlPdfPage Class

    /// <summary>
    /// A page to insert into a HtmlToPdfBuilder Class
    /// </summary>
    public class HtmlPdfPage
    {

        #region Constructors

        /// <summary>
        /// The default information for this page
        /// </summary>
        public HtmlPdfPage()
        {
            _Html = new StringBuilder();
        }

        #endregion

        #region Fields

        //parts for generating the page
        internal StringBuilder _Html;

        #endregion

        #region Working With The Html

        /// <summary>
        /// Appends the formatted HTML onto a page
        /// </summary>
        public virtual void AppendHtml(string content, params object[] values)
        {
            this._Html.AppendFormat(content, values);
        }

        #endregion

    }

    #endregion

    #region Rendering Delegate

    /// <summary>
    /// Delegate for rendering events
    /// </summary>
    public delegate void RenderEvent(PdfWriter writer, Document document);

    #endregion

}
