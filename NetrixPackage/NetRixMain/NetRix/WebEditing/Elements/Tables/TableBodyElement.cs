using GuruComponents.Netrix.ComInterop;

namespace GuruComponents.Netrix.WebEditing.Elements
{
    /// <summary>
    /// Represents the body (TBODY) of a table.
    /// </summary>
    /// <remarks>
    /// The MSHTML editor creates allways fully HTML 4.0 compatibel tables which have a TBODY tag
    /// in it that contains the row and cell definitions. This tag cannot be forced or avoid. To 
    /// help the user to use the UI of the host application to format the table using any of the
    /// section elements THEAD, TBODY or TFOOT this class can be use.
    /// </remarks>
    public sealed class TableBodyElement : TableSectionElement
    {

                /// <summary>
        /// Creates the specified element.
        /// </summary>
        /// <remarks>
        /// The element is being created and attached to the current document, but nevertheless not visible,
        /// until it's being placed anywhere within the DOM. To attach an element it's possible to either
        /// use the <see cref="ElementDom"/> property of any other already placed element and refer to this
        /// DOM or use the body element (<see cref="HtmlEditor.GetBodyElement"/>) and add the element there. Also, in 
        /// case of user interactive solutions, it's possible to add an element near the current caret 
        /// position, using <see cref="HtmlEditor.CreateElementAtCaret(string)"/> method.
        /// <para>
        /// Note: Invisible elements do neither appear in the DOM nor do they get saved.
        /// </para>
        /// </remarks>
        /// <param name="editor">The editor this element belongs to.</param>
        public TableBodyElement(IHtmlEditor editor)
            : base("tbody", editor)
        {
        }

        internal TableBodyElement (Interop.IHTMLElement peer, IHtmlEditor editor) : base (peer, editor)
        {
        } 
    }
}
