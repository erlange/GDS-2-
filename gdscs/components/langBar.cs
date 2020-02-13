namespace gds
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class langBar : UserControl
    {
        [AccessedThroughProperty("DropDownList1")]
        private DropDownList _DropDownList1;
        private object designerPlaceholderDeclaration;

        [DebuggerNonUserCode]
        public langBar()
        {
            base.Load += new EventHandler(this.Page_Load);
            base.Init += new EventHandler(this.Page_Init);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
        }

        private void Page_Init(object sender, EventArgs e)
        {
            this.InitializeComponent();
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        protected virtual DropDownList DropDownList1
        {
            [DebuggerNonUserCode]
            get
            {
                return this._DropDownList1;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._DropDownList1 = value;
            }
        }
    }
}

