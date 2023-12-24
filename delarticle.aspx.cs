using System;
using System.Xml;

public partial class delarticle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int32 id = Convert.ToInt32(Request["id"]);

            string xmlFilePath = Server.MapPath("/db/data.xml");

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(xmlFilePath);

            var objArticle = xmldoc.SelectSingleNode("articles").SelectSingleNode("article[@id=" + id + "]");

            objArticle.ParentNode.RemoveChild(objArticle);

            xmldoc.Save(xmlFilePath);

            Response.Redirect("/");

        }
    }
}