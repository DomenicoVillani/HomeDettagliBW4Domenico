using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace BW4
{
    public partial class Home : System.Web.UI.Page
    {
        public static List<Articolo> allValues = new List<Articolo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            DBConn.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Prodotti", DBConn.conn);
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                Articolo articolo = new Articolo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDecimal(3), reader.GetString(4), reader.GetString(5));
                allValues.Add(articolo);
            }
            if (!IsPostBack)
            {
                RepeaterBackoffice.DataSource = allValues;
                RepeaterBackoffice.DataBind();
            }
            DBConn.conn.Close();
        }
        public class Articolo
        {
            public int Id { get; set; }
            public string Descrizione { get; set; }
            public string Dettagli { get; set; }
            public decimal Prezzo { get; set; }
            public string Nome { get; set; }
            public string Immagine { get; set; }


            public Articolo(int id, string descrizione, string dettagli, decimal prezzo, string nome, string immagine)
            {
                Immagine = immagine;
                Descrizione = descrizione;
                Dettagli = dettagli;
                Nome = nome;
                Id = id;
                Prezzo = prezzo;
            }
        }

        protected void AddToCart(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string productId = btn.CommandArgument;
            int prodId = int.Parse(productId);
            List<int> products;
            if (Session["ProductID"] == null)
            {
                products = new List<int>();
                Session["ProductID"] = products;
            }
            else
            {
                products = (List<int>)Session["ProductID"];
            }
            products.Add(prodId);
            Session["ProductID"] = products;
            Response.Write(products.Count);
        }
    }
}