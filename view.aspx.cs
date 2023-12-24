using System;
using System.Web.UI;
using System.Xml;

public partial class view : System.Web.UI.Page
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

            litarticletitle.Text = objArticle.SelectSingleNode("articletitle").InnerText;
            litarticleauthor.Text = objArticle.SelectSingleNode("articleauthor").InnerText;
            litarticlebody.Text = objArticle.SelectSingleNode("articlebody").InnerText;
            litarticlecreatedonutc.Text = objArticle.SelectSingleNode("createdonutc").InnerText;

            if (objArticle.SelectSingleNode("articleimage").InnerText != "")
            {
                divimage.Visible = true;
                imgarticle.ImageUrl = objArticle.SelectSingleNode("articleimage").InnerText;
            }

            Page.Title = litarticletitle.Text;

        }
    }
}