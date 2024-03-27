using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace IRIPortal.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //var _config = app.Services.GetRequiredService<IConfiguration>();
            //var connectionstring = _config.GetConnectionString("WarehouseConnectionString");
            var _conn = "Data Source=10.110.21.229;Initial Catalog=Warehouse_prod;user Id=bi41user_prod;password=zqmoxwNI12;TrustServerCertificate=True;Integrated Security=false;";

            List<result> results = new List<result>();
            //using (SqlConnection conn = new SqlConnection(_conn))
            //{

            //    conn.Open();
            //    SqlDataReader dr;
            //    SqlCommand cmd = new SqlCommand("BT_getSomeData", conn);

            //    cmd.CommandType = CommandType.StoredProcedure;

            //    dr = cmd.ExecuteReader();

            //    while (dr.Read())
            //    {
            //        badSchedules.Add(dr["full_name"].ToString() ?? "EMPTY");
            //    }
            //    dr.Close();
            //    conn.Close();
            //}
            ViewData["results"] = results;
        }

        public class result
        {
            public string? fullname { get; set; }
            public string? postcode { get; set; }
            public string? email { get; set; }
            public string? telephone { get; set; }
            public string? town { get; set; }
            public string? persontype { get; set; }
        }
        public void OnPost(string surname, string telephone, string email, string postcode)
        {
            //var _config = app.Services.GetRequiredService<IConfiguration>();
            //var connectionstring = _config.GetConnectionString("WarehouseConnectionString");
            var _conn = "Data Source=10.110.21.229;Initial Catalog=Warehouse_prod;user Id=bi41user_prod;password=zqmoxwNI12;TrustServerCertificate=True;Integrated Security=false;";

            List<result> results = new List<result>();
            using (SqlConnection conn = new SqlConnection(_conn))
            {

                conn.Open();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("BT_search", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("surname", SqlDbType.VarChar,100).Value= surname;
                cmd.Parameters.Add("email", SqlDbType.VarChar, 100).Value = email;
                cmd.Parameters.Add("telephone", SqlDbType.VarChar, 100).Value = telephone;
                cmd.Parameters.Add("postcode", SqlDbType.VarChar, 100).Value = postcode;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    result r = new();
                    r.email = dr["email"].ToString();
                    r.telephone = dr["telephone"].ToString();
                    r.fullname = dr["full_name"].ToString();
                    r.postcode = dr["postcode"].ToString();
                    r.town = dr["town"].ToString();
                    r.persontype = dr["person_type"].ToString();
                    results.Add(r);
                }
                dr.Close();
                conn.Close();
            }
            ViewData["results"] = results;
        }
    }
}