using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Linq;

namespace ConsultasListas.VisualWebPart1
{
    [ToolboxItemAttribute(false)]
    public partial class VisualWebPart1 : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public VisualWebPart1()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SPSiteDataQuery query = new SPSiteDataQuery();

            query.Webs = @"<Webs scope=""Recursive"">";
            query.Lists = @"<Lists ServerTemplate=""105"">";
            query.Query = "<Where><Eq><FieldRef Name=\"Last Name\" />" + "<value Type='Text'>" + txtApellidos.Text + "</value>" + "</Eq></Where>";

            var res = SPContext.Current.Web.GetSiteData(query);

            var datos = res.CreateDataReader();

            lblResultado.Text = "";
            while (datos.Read())

                for (int i = 0; i < datos.FieldCount; i++)
                {
                    lblResultado.Text += datos.GetValue(i).ToString();
                }

            lblResultado.Text += "<br/>";
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            using (var context = new TeamsiteDataContext("http://localhost"))
            {
                var datos = from o in context.Contactos
                    where o.LastName.Contains(txtApellidos.Text) select o;
                lblResultado.Text = "";
                foreach (var dato in datos)
                {
                    lblResultado.Text += dato.FirstName + "<br/>";
                }
            }
        }
    }
}
