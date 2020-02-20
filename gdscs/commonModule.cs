using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
//using Microsoft.VisualBasic;
namespace gds
{
    public static class commonModule
    {
        public static string FILETEMPVIRTUALPATH = "upload/";
        public static string IMGDESTVIRTUALPATH = "gallery/";
        public static string DOCDESTVIRTUALPATH = "docs/";
        public static string IMGNAVIRTUALPATH = "imgedit/imgna.jpg";

        public static string NEXTSTRING = "Lanjut >>";
        public static string PREVSTRING = "<< Kembali";
        public static string NEXTSTRINGEN = "Next >>";
        public static string PREVSTRINGEN = "<< Back";
        public static void RedirectError(Exception ex)
        {
            HttpContext.Current.Session["errMsg"] = "Exception: " + DateTime.Now + (char)10 + ex.Message + (char)10 + System.Web.HttpContext.Current.Request.Url.ToString() + (char)10 + ex.StackTrace;
            HttpContext.Current.Response.Redirect("err.aspx");
        }

        public static string Right(string original, int numberCharacters)
        {
            return original.Substring(original.Length - numberCharacters);
        }
        public static string ConvertBytes(long Bytes)
        {
            // Converts bytes into a readable "1.44 MB"
            if (Bytes >= 1073741824)
                return (Bytes / (double)1024 / 1024 / 1024).ToString("#0.00") + " GB";
            else if (Bytes >= 1048576)
                return (Bytes / (double)1024 / 1024).ToString("#0.00") + " MB";
            else if (Bytes >= 1024)
                return (Bytes / (double)1024).ToString("#0.00") + " KB";
            else if (Bytes > 0 & Bytes < 1024)
                return Bytes + " Bytes ";
            else
                return "0 Bytes";
        }
        public static string SetDbSafeString(string inputStr)
        {
            string s;
            s = HttpContext.Current.Server.HtmlEncode(inputStr.Trim());
            s = s.Replace("'", "''");
            s = s.Replace("\"", "&quot;");
            return s;
        }
        public static string GetDbSafeString(string inputStr)
        {
            return HttpContext.Current.Server.HtmlDecode(inputStr.Trim());
        }

        public static bool IsInAdminsRole()
        {
            if (HttpContext.Current.User.IsInRole("Admins"))
                return true;
            else
                return false;
        }
        public static bool IsEnglish()
        {
            if (HttpContext.Current.Session["en"] != null)
            {
                if (HttpContext.Current.Session["en"].ToString() == "1")
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        public static string GetChartFooterString()
        {
            return "Governance and Decentralization Survey 2 - Indonesia";
        }
        public static string GetConnString()
        {
            return ConfigurationManager.AppSettings["sqlConnStr"];
        }
        public static DataTable GetData(string connString, string sql)
        {
            SqlConnection cn = new SqlConnection(connString);
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public static string WriteXLS(params Control[] ctl)
        {
            //Control c;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            foreach (Control c in ctl)
                c.RenderControl(hw);
            // Me.Literal3.RenderControl(hw)
            // ctl.RenderControl(hw)
            // Me.Literal1.RenderControl(hw)
            // Put user code to initialize the page here


            return tw.ToString();
        }
        public static void DownloadExcel(string fileName, params System.Web.UI.Control[] controls)
        {
            string s;
            s = commonModule.WriteXLS(controls);
            {
                var response = HttpContext.Current.Response;
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("content-disposition", "inline; filename=" + fileName);
                response.Charset = "";
                response.Write(s);
                response.Flush();
                response.End();
            }
        }


        public static string s(string input, int w, int fontSize)
        {
            string sResult = "";
            string[] ss = input.Split(' ');
            int j = 0;
            for (int i = 0; i <= ss.Length - 1; i++)
            {
                if ((j * fontSize) < w)
                {
                    j += ss[i].Length;
                    j += 1;
                    sResult = ss[i] + " ";
                }
                else
                {
                    sResult += Environment.NewLine;
                    if (sResult.Split((char)10).Length > 0)
                        sResult = s(sResult.Split((char)10)[1], w, fontSize);
                    break;
                }
            }
            //sResult += Strings.Right(input, (input.Length + 1) - j);
            sResult += commonModule.Right(input, (input.Length + 1) - j);
            return sResult;
        }

        public static string FormatByLength(string Expression, long Length)
        {
            string[] BufferCrLf;
            string[] BufferSpace;
            string Buffer = "";
            long k = 0;
            long j = 0;
            long count = 0;
            BufferCrLf = Expression.Split((char)10);
            for (k = 0; k <= BufferCrLf.Length - 1; k++)
            {

                if (BufferCrLf[k].Length <= Length)
                    Buffer = Buffer + BufferCrLf[k] + (char)10;
                else
                {
                    BufferSpace = BufferCrLf[k].Split(' ');
                    for (j = 0; j <= BufferSpace.Length - 1; j++)
                    {
                        count += BufferSpace[j].Length + 1;
                        if ((count <= Length))
                            Buffer = Buffer + BufferSpace[j] + " ";
                        else
                        {
                            count = 0;
                            Buffer = Buffer + (char)10 + BufferSpace[j] + " ";
                            count = BufferSpace[j].Length + 1;
                        }
                    }
                    Buffer = Buffer + (char)10;
                }
            }
            return Buffer;
        }

        public static string WrapString(string text, int width, int fontSize)
        {
            string sResult = "";
            string[] aSt = text.Split(' ');
            int ii = 0;
            for (int i = 0; i <= aSt.Length - 1; i++)
            {
                if ((ii * fontSize) < width)
                {
                    ii += aSt[i].Length;
                    ii += 1;
                    sResult += aSt[i] + " ";
                }
                else
                {
                    sResult += (char)10;
                    break;
                }
            }

            sResult += commonModule.Right(text, (text.Length + 1) - ii);
            return sResult;
        }
        enum BasicCompType : int
        {
            District = 0,
            Province = 1,
            National = 2,
            Geographical = 4 // Other comparators
    ,
            OthersCompInDistProv = 8 // Other comparators in prov & kabu
    ,
            OthersCompInProv = 16 // Other comparators in prov & kabu
    ,
            Other = 32 // Other comparators
        }

        public static byte[] EncryptString(string strToEncrypt)
        {
            System.Security.Cryptography.SHA512Managed des = new System.Security.Cryptography.SHA512Managed();
            byte[] inBytes;
            byte[] outBytes;

            inBytes = System.Text.Encoding.ASCII.GetBytes(strToEncrypt);
            outBytes = des.ComputeHash(inBytes);
            return outBytes;
        }
    }

    public class gdsHighlight
    {
        public string VarDesc
        {
            get
            {
                return _varDesc;
            }
        }
        public string VarDescEn
        {
            get { return _varDescEn; }
        }

        public string DataSetDesc
        {
            get { return _dataSetDesc; }
        }
        public string DataSetDescEn
        {
            get { return _dataSetDescEn; }
        }
        private string _dataSetDesc;
        private string _dataSetDescEn;
        private string _varDescEn;
        private string _varDesc;

        public DataTable GetSurveyHighlight(int gdsId, int varId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cm;
            {

                cm.CommandText = "GetSurveyHighlight";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                {
                    cm.Parameters.Add("@gdsid", SqlDbType.TinyInt);
                    cm.Parameters.Add("@varid", SqlDbType.Int);
                    cm.Parameters.Add("@gdsdesc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@gdsdesc_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@vardesc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@vardesc_en", SqlDbType.VarChar, 255);
                }
                cm.Parameters["@gdsid"].Value = gdsId;
                cm.Parameters["@varid"].Value = varId;
                cm.Parameters["@gdsdesc"].Value = _dataSetDesc;
                cm.Parameters["@gdsdesc_en"].Value = _dataSetDescEn;
                cm.Parameters["@vardesc"].Value = _varDesc;
                cm.Parameters["@vardesc_en"].Value = _varDescEn;

                cm.Parameters["@gdsdesc"].Direction = ParameterDirection.Output;
                cm.Parameters["@gdsdesc_en"].Direction = ParameterDirection.Output;
                cm.Parameters["@vardesc"].Direction = ParameterDirection.Output;
                cm.Parameters["@vardesc_en"].Direction = ParameterDirection.Output;
            }
            try
            {
                da.Fill(ds);

                {

                    _dataSetDesc = cm.Parameters["@gdsdesc"].Value.ToString();
                    _dataSetDescEn = cm.Parameters["@gdsdesc_en"].Value.ToString();
                    _varDesc = cm.Parameters["@vardesc"].Value.ToString();
                    _varDescEn = cm.Parameters["@vardesc_en"].Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }

            return ds.Tables[0];
        }
        public DataTable GetSurveyHighlightByIsland(int gdsId, int varId, int islandId = 0, bool isEnglish = false)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataReader dr;
            {
                cm.CommandText = "GetSurveyHighlightByIsland";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                {
                    cm.Parameters.Add("@gdsid", SqlDbType.TinyInt);
                    cm.Parameters.Add("@varid", SqlDbType.Int);
                    cm.Parameters.Add("@islandid", SqlDbType.Int);
                    cm.Parameters.Add("@gdsdesc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@gdsdesc_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@vardesc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@vardesc_en", SqlDbType.VarChar, 255);
                }
                cm.Parameters["@gdsid"].Value = gdsId;
                cm.Parameters["@varid"].Value = varId;
                cm.Parameters["@islandid"].Value = islandId;
                cm.Parameters["@gdsdesc"].Value = _dataSetDesc;
                cm.Parameters["@gdsdesc_en"].Value = _dataSetDescEn;
                cm.Parameters["@vardesc"].Value = _varDesc;
                cm.Parameters["@vardesc_en"].Value = _varDescEn;

                cm.Parameters["@gdsdesc"].Direction = ParameterDirection.Output;
                cm.Parameters["@gdsdesc_en"].Direction = ParameterDirection.Output;
                cm.Parameters["@vardesc"].Direction = ParameterDirection.Output;
                cm.Parameters["@vardesc_en"].Direction = ParameterDirection.Output;
            }

            dt.Columns.Add("desc", typeof(string));
            dt.Columns.Add("CountNatl", typeof(int));

            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                DataRow drw;
                while (dr.Read())
                {
                    drw = dt.NewRow();
                    if (isEnglish)
                        drw["desc"] = Convert.IsDBNull(dr["desc_en"]) ? "" : dr["desc_en"];
                    else
                        drw["desc"] = Convert.IsDBNull(dr["desc"]) ? "" : dr["desc"];
                    drw["CountNatl"] = Convert.IsDBNull(dr["CountNatl"]) ? 0 : dr["CountNatl"];
                    dt.Rows.Add(drw);
                }

                cn.Close();
                {

                    _dataSetDesc = cm.Parameters["@gdsdesc"].Value.ToString();
                    _dataSetDescEn = cm.Parameters["@gdsdesc_en"].Value.ToString();
                    _varDesc = cm.Parameters["@vardesc"].Value.ToString();
                    _varDescEn = cm.Parameters["@vardesc_en"].Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }

            return dt;
        }
        public DataTable GetSurveyHighlightPie(int gdsId, int varId, bool isEnglish)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataReader dr;
            {

                cm.CommandText = "GetSurveyHighlight";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                {
                    cm.Parameters.Add("@gdsid", SqlDbType.TinyInt);
                    cm.Parameters.Add("@varid", SqlDbType.Int);
                    cm.Parameters.Add("@gdsdesc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@gdsdesc_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@vardesc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@vardesc_en", SqlDbType.VarChar, 255);
                }
                cm.Parameters["@gdsid"].Value = gdsId;
                cm.Parameters["@varid"].Value = varId;
                cm.Parameters["@gdsdesc"].Value = _dataSetDesc;
                cm.Parameters["@gdsdesc_en"].Value = _dataSetDescEn;
                cm.Parameters["@vardesc"].Value = _varDesc;
                cm.Parameters["@vardesc_en"].Value = _varDescEn;

                cm.Parameters["@gdsdesc"].Direction = ParameterDirection.Output;
                cm.Parameters["@gdsdesc_en"].Direction = ParameterDirection.Output;
                cm.Parameters["@vardesc"].Direction = ParameterDirection.Output;
                cm.Parameters["@vardesc_en"].Direction = ParameterDirection.Output;
            }

            dt.Columns.Add("desc", typeof(string));
            dt.Columns.Add("CountNatl", typeof(int));

            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                DataRow drw;
                while (dr.Read())
                {
                    drw = dt.NewRow();
                    if (isEnglish)
                        drw["desc"] = Convert.IsDBNull(dr["desc_en"])? "":dr["desc_en"];
                    else
                        drw["desc"] = Convert.IsDBNull(dr["desc"])?"":dr["desc"];
                    drw["CountNatl"] = Convert.IsDBNull(dr["CountNatl"]) ? 0 : dr["CountNatl"];
                    dt.Rows.Add(drw);
                }

                cn.Close();
                {

                    _dataSetDesc = cm.Parameters["@gdsdesc"].Value.ToString();
                    _dataSetDescEn = cm.Parameters["@gdsdesc_en"].Value.ToString();
                    _varDesc = cm.Parameters["@vardesc"].Value.ToString();
                    _varDescEn = cm.Parameters["@vardesc_en"].Value.ToString();
                }
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }

            return dt;
        }

        public gdsHighlight()
        {
        }
    }

    class gdsGeo
    {
        private string _prov;
        private string _provnm;
        private string _kabu;
        private string _kabunm;
        public gdsGeo(string prov, string kabu)
        {
            string sql = "SELECT     t01prov.prov AS prov , provnm, kabu, kabunm " + " FROM t01prov INNER JOIN " + " t02kabu ON t01prov.prov = t02kabu.prov " + " WHERE t01prov.prov = '" + prov + "' " + (kabu != "All" ? " AND  kabu = '" + kabu + "' " : "");
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand(sql, cn);

            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _prov = Convert.IsDBNull(dr["prov"])? "":dr["prov"].ToString();
                    _provnm = Convert.IsDBNull(dr["provnm"])? "": dr["provnm"].ToString();
                    _kabu = Convert.IsDBNull(dr["kabu"])? "": dr["kabu"].ToString();
                    _kabunm = Convert.IsDBNull(dr["kabunm"]) ? "" : dr["kabunm"].ToString();
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
        }

        public string Prov
        {
            get { return _prov; }
        }
        public string ProvName
        {
            get { return _provnm; }
        }
        public string Kabu
        {
            get { return _kabu; }
        }
        public string KabuName
        {
            get { return _kabunm; }
        }
    }

    class gdsVar
    {
        private int _var_id;
        private string _var;
        private int _var_parent;
        private string _var_parent_desc;
        private string _var_parent_desc_en;
        private int _ord;
        private string _desc;
        private string _desc_en;
        private string _q;
        private string _q_en;
        private string _tbl;
        private string _tbl_id;
        private int _lvl;
        private bool _isVisible;
        private bool _isContVar;
        private bool _isAdvance;
        private string _criteria;
        public gdsVar(int var_id, string varTbl)
        {
            string s = " SELECT * FROM " + varTbl + " WHERE var_id = " + var_id;
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand(s, cn);

            cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                _var_id = int.Parse(dr["var_id"].ToString());
                _var_parent = int.Parse(dr["var_parent"].ToString());
                _var = dr["var"].ToString();
                _desc = dr["desc"].ToString();
                _desc_en = Convert.IsDBNull(dr["desc_en"]) ? _desc : dr["desc_en"].ToString();
                _q = Convert.IsDBNull(dr["q"]) ? _desc : dr["q"].ToString();
                _q_en = Convert.IsDBNull(dr["q_en"]) ? _desc_en : dr["q_en"].ToString();
                _tbl = dr["tbl"].ToString();
                _tbl_id = dr["tbl_id"].ToString();
                _lvl = int.Parse(dr["lvl"].ToString());
                _isVisible = System.Convert.ToBoolean(dr["isVisible"]);
                _isContVar = System.Convert.ToBoolean(dr["isContVar"]);
                _isAdvance = System.Convert.ToBoolean(dr["isAdvance"]);
                _criteria = Convert.IsDBNull(dr["criteria"]) ? "" : dr["criteria"].ToString();
            }
            cm.CommandText = "SELECT * FROM " + varTbl + " WHERE var_id =" + (Convert.IsDBNull(dr["var_parent"]) ? "" : _var_parent.ToString());
            dr.Close();
            dr = cm.ExecuteReader();

            if (dr.Read())
            {
                _var_parent_desc = dr["desc"].ToString();
                _var_parent_desc_en = Convert.IsDBNull(dr["desc_en"]) ? _var_parent_desc : dr["desc_en"].ToString();
            }
            dr.Close();
            cn.Close();
        }

        public int Var_Id
        {
            get { return _var_id; }
        }
        public int Var_Parent
        {
            get { return _var_parent; }
        }
        public string Var_Parent_Desc
        {
            get { return _var_parent_desc; }
        }
        public string Var_Parent_Desc_En
        {
            get { return _var_parent_desc_en; }
        }
        public int Ord
        {
            get { return _ord; }
        }
        public string Var
        {
            get { return _var; }
        }
        public string Desc
        {
            get { return _desc; }
        }
        public string Desc_En
        {
            get { return _desc_en; }
        }
        public string Q
        {
            get { return _q; }
        }
        public string Q_En
        {
            get { return _q_en; }
        }
        public string Tbl
        {
            get { return _tbl; }
        }
        public string Tbl_Id
        {
            get { return _tbl_id; }
        }
        public bool IsVisible
        {
            get { return _isVisible; }
        }
        public bool IsContVar
        {
            get { return _isContVar; }
        }
        public bool IsAdvance
        {
            get { return _isAdvance; }
        }
        public string Criteria
        {
            get { return _criteria; }
        }
    }

    public class gdsTable
    {
        private string _varTable;
        private string _valueTable;
        private string _avTable;
        private string _compTable;
        private int _gdsId;
        private string _desc;
        private string _desc_En;
        private string _kabufld;
        private string _kabunmfld;
        private string _provfld;
        private string _provnmfld;
        private string _baseTable;
        private bool _hasKeca;


        public gdsTable()
        {
        }
        public gdsTable(int gdsId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand(" SELECT * FROM gds2 WHERE gds_id = " + gdsId, cn);
            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();

                if (dr.Read())
                {
                    _gdsId = int.Parse(dr["gds_id"].ToString());
                    _varTable = dr["vartbl"].ToString();
                    _valueTable = dr["valuetbl"].ToString();
                    _avTable = dr["avtbl"].ToString();
                    _compTable = dr["comptbl"].ToString();
                    _desc = dr["desc"].ToString();
                    _desc_En = dr["desc_en"].ToString();
                    _kabufld = dr["kabufld"].ToString();
                    _kabunmfld = dr["kabunmfld"].ToString();
                    _provfld = dr["provfld"].ToString();
                    _provnmfld = dr["provnmfld"].ToString();
                    _baseTable = dr["baseTbl"].ToString();
                    // _hasKeca = IIf(Int32.Parse(dr("hasKeca")) = 1, True, False)
                    _hasKeca = bool.Parse(dr["hasKeca"].ToString());
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
        }
        public int GdsId
        {
            get { return _gdsId; }
        }
        public string VarTable
        {
            get { return _varTable; }
        }
        public string ValueTable
        {
            get { return _valueTable; }
        }
        public string AvTable
        {
            get { return _avTable; }
        }
        public string CompTable
        {
            get { return _compTable; }
        }
        public string Desc
        {
            get { return _desc; }
        }
        public string Desc_En
        {
            get { return _desc_En; }
        }
        public string ProvFld
        {
            get { return _provfld; }
        }
        public string ProvNmFld
        {
            get { return _provnmfld; }
        }
        public string KabuFld
        {
            get { return _kabufld; }
        }
        public string KabuNmFld
        {
            get { return _kabunmfld; }
        }
        public string BaseTable
        {
            get { return _baseTable; }
        }
        public bool HasKeca
        {
            get { return _hasKeca; }
        }
        public DataTable GetGds2Var()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cm = new SqlCommand();
            cm.CommandText = "getGds2Var";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Connection = cn;
            da.SelectCommand = cm;
            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }
    }

    public class gdsCmsPanel
    {
        public string Title
        {
            get { return _Title; }
        }
        public string TitleEn
        {
            get { return _TitleEn; }
        }

        public string Content
        {
            get { return _Content; }
        }
        public string ContentEn
        {
            get { return _ContentEn; }
        }
        public int panelId
        {
            get { return _panelId; }
        }

        private string _Title;
        private string _Content;
        private string _TitleEn;
        private string _ContentEn;
        private int _panelId;
        public gdsCmsPanel(int panelId, Language language)
        {
            switch (language)
            {
                case Language.English:
                    {
                        GetContentEn(panelId);
                        break;
                    }

                case Language.Indonesian:
                    {
                        GetContentIna(panelId);
                        break;
                    }
            }
            _panelId = panelId;
        }
        public gdsCmsPanel(int panelId)
        {
            GetContent(panelId);
            _panelId = panelId;
        }
        public gdsCmsPanel()
        {
        }

        private void GetContentEn(int panelId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            {
                cm.CommandText = "GetContentEn";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                cm.Parameters.Add("@panelid", SqlDbType.Int);
                cm.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Title = Convert.IsDBNull(dr["title_en"]) ? "" : dr["title_en"].ToString();
                    _Content = Convert.IsDBNull(dr["content_en"]) ? "" : dr["content_en"].ToString();
                }
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
        }
        private void GetContentIna(int panelId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            {
                cm.CommandText = "GetContentIna";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                cm.Parameters.Add("@panelid", SqlDbType.Int);
                cm.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Title = Convert.IsDBNull(dr["title"]) ? "" : dr["title"].ToString();
                    _Content = Convert.IsDBNull(dr["content"]) ? "" : dr["content"].ToString();
                }
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
        }
        public int UpdateContent(int panelId, string title, string title_en, string content, string content_en)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;
            {
                cm.CommandText = "UpdateContent";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                {
                    cm.Parameters.Add("@panelid", SqlDbType.Int);
                    cm.Parameters.Add("@title", SqlDbType.VarChar, 50);
                    cm.Parameters.Add("@title_en", SqlDbType.VarChar, 50);
                    cm.Parameters.Add("@content", SqlDbType.Text);
                    cm.Parameters.Add("@content_en", SqlDbType.Text);
                }

                cm.Parameters["@panelid"].Value = panelId;
                cm.Parameters["@title"].Value = title.Length == 0 ? SqlString.Null : title;
                cm.Parameters["@title_en"].Value = title_en.Length == 0 ? SqlString.Null : title_en;
                cm.Parameters["@content"].Value = content.Length == 0 ? SqlString.Null : content;
                cm.Parameters["@content_en"].Value = content_en.Length == 0 ? SqlString.Null : content_en;
            }
            try
            {
                cn.Open();
                iRecordsAffected = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }

        public int GetContent(int panelId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            {
                cm.CommandText = "GetContent";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                cm.Parameters.Add("@panelid", SqlDbType.Int);
                cm.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Title = Convert.IsDBNull(dr["title"]) ? "" : dr["title"].ToString();
                    _TitleEn = Convert.IsDBNull(dr["title_en"]) ? "" : dr["title_en"].ToString();
                    _Content = Convert.IsDBNull(dr["content"]) ? "" : dr["content"].ToString();
                    _ContentEn = Convert.IsDBNull(dr["content_en"]) ? "" : dr["content_en"].ToString();
                    iRecordsReturned = 1;
                }
                else
                    iRecordsReturned = 0;
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
        public DataTable GetMaxComments(int maxRecords = 0, bool isVisible = true)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetMaxComments";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                {
                    cm.Parameters.Add("@maxrecords", SqlDbType.Int);
                    cm.Parameters.Add("@isvisible", SqlDbType.TinyInt);
                }
                cm.Parameters["@maxrecords"].Value = maxRecords;
                cm.Parameters["@isVisible"].Value = isVisible;
            }
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }
        public DataTable GetTop5Comments()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            {
                cm.CommandText = "GetTop5Comments";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
            }
            da.SelectCommand = cm;
            try
            {
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return ds.Tables[0];
        }
        public DataTable GetAllComments()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            {
                cm.CommandText = "GetAllComments";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
            }
            da.SelectCommand = cm;
            try
            {
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return ds.Tables[0];
        }
        public int InsertComment(string sender, string email, string url, string comment, DateTime submitdate, int isvisible)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;
            {

                cm.CommandText = "InsertComment";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                {
                    
                    cm.Parameters.Add("@sender", SqlDbType.VarChar, 50);
                    cm.Parameters.Add("@email", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@url", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@comment", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@submitdate", SqlDbType.SmallDateTime);
                    cm.Parameters.Add("@isvisible", SqlDbType.TinyInt);
                }
                cm.Parameters["@sender"].Value = sender.Length == 0? SqlString.Null: sender;
                cm.Parameters["@email"].Value = email.Length == 0 ? SqlString.Null : email;
                cm.Parameters["@url"].Value = url.Length == 0 ? SqlString.Null : url;
                cm.Parameters["@comment"].Value = comment.Length == 0 ? SqlString.Null : comment;
                cm.Parameters["@isvisible"].Value = isvisible;
                cm.Parameters["@submitdate"].Value = submitdate;
            }
            try
            {
                cn.Open();
                iRecordsAffected = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }

        public int UpdateComment(int commentid, string sender, string email, string url, string comment, DateTime submitdate, int isvisible)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;
            {
                
                cm.CommandText = "UpdateComment";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                {
                    cm.Parameters.Add("@commentid", SqlDbType.Int);
                    cm.Parameters.Add("@sender", SqlDbType.VarChar, 50);
                    cm.Parameters.Add("@email", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@url", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@comment", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@submitdate", SqlDbType.SmallDateTime);
                    cm.Parameters.Add("@isvisible", SqlDbType.TinyInt);
                }
                cm.Parameters["@commentid"].Value = commentid;
                cm.Parameters["@sender"].Value = sender.Length == 0 ? SqlString.Null : sender;
                cm.Parameters["@email"].Value = email.Length == 0 ? SqlString.Null : email;
                cm.Parameters["@url"].Value = url.Length == 0 ? SqlString.Null : url;
                cm.Parameters["@comment"].Value = comment.Length == 0 ? SqlString.Null : comment;
                cm.Parameters["@isvisible"].Value = isvisible;
                cm.Parameters["@submitdate"].Value = submitdate;
            }
            try
            {
                cn.Open();
                iRecordsAffected = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }

        public int DeleteComment(int commentId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;
            {
                cm.CommandText = "DeleteComment";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@commentid", SqlDbType.Int);
                cm.Parameters["@commentid"].Value = commentId;
            }
            try
            {
                cn.Open();
                iRecordsAffected = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }
        public int GetGenericContentByIdLang(int panelId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;
            {
                cm.CommandText = "GetGenericContentByIdLang";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                cm.Parameters.Add("@panelid", SqlDbType.Int);
                cm.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Content = Convert.IsDBNull(dr["generictext"])? "":dr["generictext"].ToString();
                    _ContentEn = Convert.IsDBNull(dr["generictext_en"]) ? "" : dr["generictext_en"].ToString();
                    iRecordsAffected = 1;
                }
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }
        public int GetGenericContentById(int panelId, bool inEnglish)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;

            {
                cm.CommandText = "GetGenericContentById";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                cm.Parameters.Add("@panelid", SqlDbType.Int);
                cm.Parameters.Add("@en", SqlDbType.TinyInt);
                cm.Parameters["@panelid"].Value = panelId;
                cm.Parameters["@en"].Value = inEnglish ? 1 : 0;
            }
            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    if (inEnglish)
                        _Content = Convert.IsDBNull(dr["generictext_en"]) ? "" : dr["generictext_en"].ToString();
                    else
                        _Content = Convert.IsDBNull(dr["generictext"]) ? "" : dr["generictext"].ToString();

                    iRecordsAffected = dr.RecordsAffected;

                }
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }
        public int UpdateGenericContentById(int panelId, string genericText, string genericTextEn)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;
            {
                cm.CommandText = "UpdateGenericContentById";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                {
                    cm.Parameters.Add("@panelid", SqlDbType.Int);
                    cm.Parameters.Add("@generictext", SqlDbType.Text);
                    cm.Parameters.Add("@generictext_en", SqlDbType.Text);
                }
                cm.Parameters["@panelid"].Value = panelId;
                cm.Parameters["@generictext"].Value = genericText.Length == 0 ? SqlString.Null : genericText;
                cm.Parameters["@generictext_en"].Value = genericTextEn.Length == 0 ? SqlString.Null : genericTextEn;
            }
            try
            {
                cn.Open();
                iRecordsAffected = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }

        public int GetGenericTitleByIdLang(int panelId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;
            {
                cm.CommandText = "GetGenericTitleByIdLang";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                cm.Parameters.Add("@panelid", SqlDbType.Int);
                cm.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Title = Convert.IsDBNull(dr["generictitle"]) ? "" : dr["generictitle"].ToString();
                    _TitleEn = Convert.IsDBNull(dr["generictitle_en"]) ? "" : dr["generictitle_en"].ToString();
                    iRecordsAffected = 1;
                }
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }
        public int GetGenericTitleById(int panelId, bool inEnglish)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;

            {

                cm.CommandText = "GetGenericTitleById";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                cm.Parameters.Add("@panelid", SqlDbType.Int);
                cm.Parameters.Add("@en", SqlDbType.TinyInt);
                cm.Parameters["@panelid"].Value = panelId;
                cm.Parameters["@en"].Value = inEnglish ? 1 : 0;
            }

            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    if (inEnglish)
                        _Content = Convert.IsDBNull(dr["generictitle_en"])? "":dr["generictitle_en"].ToString();
                    else
                        _Content = Convert.IsDBNull(dr["generictitle"]) ? "" : dr["generictitle"].ToString();
                    iRecordsAffected = 1;
                }
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }
        public int UpdateGenericTitleById(int panelId, string genericTitle, string genericTitleEn)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsAffected = 0;
            {

                cm.CommandText = "UpdateGenericTitleById";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                {
                    cm.Parameters.Add("@panelid", SqlDbType.Int);
                    cm.Parameters.Add("@generictitle", SqlDbType.VarChar, 50);
                    cm.Parameters.Add("@generictitle_en", SqlDbType.VarChar, 50);
                }
                cm.Parameters["@panelid"].Value = panelId;
                cm.Parameters["@generictitle"].Value = genericTitle.Length == 0 ? SqlString.Null : genericTitle;
                cm.Parameters["@generictitle_en"].Value = genericTitleEn.Length == 0 ? SqlString.Null : genericTitleEn;
            }
            try
            {
                cn.Open();
                iRecordsAffected = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsAffected;
        }
    }

    public class gdsDbAdminGds
    {
    }

    public class gdsDbAdminVar
    {
        private int _gds_id;
        private int _var_id;
        private string _var;
        private string _desc;
        private string _desc_en;
        private bool _isVisible;
        private bool _isContVar;
        private bool _isAdvance;
        private string _q;
        private string _q_en;

        public string Desc
        {
            get { return _desc; }
        }

        public string DescEn
        {
            get { return _desc_en; }
        }
        public string Var
        {
            get { return _var; }
        }
        public int GdsId
        {
            get { return _gds_id; }
        }
        public int VarId
        {
            get { return _var_id; }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
        }

        public bool IsContVar
        {
            get { return _isContVar; }
        }
        public bool IsAdvance
        {
            get { return _isAdvance; }
        }

        public string Q
        {
            get { return _q; }
        }
        public string QEn
        {
            get { return _q_en; }
        }
        public int getVarByVarId(int gdsId, int varId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;

            cm.CommandText = "getVarByVarId";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Connection = cn;

            SqlParameter paramGds_Id = new SqlParameter("@gds_id", SqlDbType.TinyInt);
            SqlParameter paramVar_Id = new SqlParameter("@var_id", SqlDbType.Int);

            paramGds_Id.Value = gdsId;
            paramVar_Id.Value = varId;

            cm.Parameters.Add(paramGds_Id);
            cm.Parameters.Add(paramVar_Id);
            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _gds_id = gdsId;
                    _var_id = varId;
                    _desc = commonModule.GetDbSafeString(Convert.IsDBNull(dr["desc"]) ? "" : dr["desc"].ToString());
                    _desc_en = commonModule.GetDbSafeString(Convert.IsDBNull(dr["desc_en"])? "":dr["desc_en"].ToString());
                    _q = commonModule.GetDbSafeString(Convert.IsDBNull(dr["q"])? "":dr["q"].ToString());
                    _q_en = commonModule.GetDbSafeString(Convert.IsDBNull(dr["q_en"])? "":dr["q_en"].ToString());
                    _var = commonModule.GetDbSafeString(Convert.IsDBNull(dr["var"])? "":dr["var"].ToString());
                    _isVisible = dr["isVisible"] == "1" ? true : false;
                    _isContVar = dr["isContVar"] == "1" ? true : false;
                    _isAdvance = dr["isAdvance"] == "1" ? true : false;
                    iRecordsReturned = 1;
                }
                else
                    iRecordsReturned = 0;
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
        public int updateVar(int gds_id, int var_id, string var, string desc, string desc_en, bool isVisible, bool isContVar, bool isAdvance, string q, string q_en)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            {

                cm.CommandText = "updateVar";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;

                cm.Parameters.Add("@gds_id", SqlDbType.TinyInt);
                cm.Parameters.Add("@var_id", SqlDbType.Int);
                cm.Parameters.Add("@desc", SqlDbType.VarChar, 255);
                cm.Parameters.Add("@desc_en", SqlDbType.VarChar, 255);
                cm.Parameters.Add("@q", SqlDbType.Text);
                cm.Parameters.Add("@q_en", SqlDbType.Text);
                cm.Parameters.Add("@var", SqlDbType.VarChar, 50);
                cm.Parameters.Add("@isVisible", SqlDbType.TinyInt);
                cm.Parameters.Add("@isContVar", SqlDbType.TinyInt);
                cm.Parameters.Add("@isAdvance", SqlDbType.TinyInt);

                cm.Parameters["@var_id"].Value = var_id;
                cm.Parameters["@desc"].Value = desc.Length == 0 ? SqlString.Null : desc;
                cm.Parameters["@desc_en"].Value = desc_en.Length == 0 ? SqlString.Null : desc_en;
                cm.Parameters["@q"].Value = q.Length == 0 ? SqlString.Null : q;
                cm.Parameters["@q_en"].Value = q_en.Length == 0 ? SqlString.Null : q_en;
                cm.Parameters["@var"].Value = var.Length == 0 ? SqlString.Null : var;
                cm.Parameters["@isVisible"].Value = isVisible ? 1 : 0;
                cm.Parameters["@isContVar"].Value = isContVar ? 1 : 0;
                cm.Parameters["@isAdvance"].Value = isAdvance ? 1 : 0;
            }
            try
            {
                cn.Open();
                iRecordsReturned = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
        public DataSet getVariablesByGdsId(int gdsId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();

            cm.CommandText = "getVariablesByGdsId";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Connection = cn;
            SqlParameter paramGds_id = new SqlParameter("@gds_id", SqlDbType.TinyInt);
            paramGds_id.Value = gdsId;
            cm.Parameters.Add(paramGds_id);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return ds;
        }
    }

    public class gdsDocumentCategories
    {
        private int _categoryid;
        private string _desc;
        private string _desc_en;
        private bool _isVisible;
        public int CategoryId
        {
            get { return _categoryid; }
        }
        public string Desc
        {
            get { return _desc; }
        }
        public string DescEn
        {
            get { return _desc_en; }
        }
        public bool IsVisible
        {
            get { return _isVisible; }
        }
        public int GetCategoryById(int categoryId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {
                cm.CommandText = "GetCategoryById";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@categoryid", SqlDbType.Int);
                cm.Parameters["@categoryid"].Value = categoryId;
            }
            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _categoryid = Convert.ToInt32(dr["categoryid"]);
                    _desc = Convert.IsDBNull(dr["desc"]) ? "" : dr["desc"].ToString();
                    _desc_en = Convert.IsDBNull(dr["desc_en"]) ? "" : dr["desc_en"].ToString();
                    _isVisible = dr["isVisible"] == "1" ? true : false;
                    iRecordsReturned = 1;
                }
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
        public DataTable GetCategories()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetCategories";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
            }
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }
    }

    public class gdsLinks
    {
        private int _linkId;
        private string _url;
        private string _title;
        private string _title_en;
        private string _desc;
        private string _desc_en;
        private DateTime _submitDate;
        private bool _isVisible;
        public int LinkId
        {
            get { return _linkId; }
        }

        public string Url
        {
            get { return _url; }
        }
        public string Title
        {
            get { return _title; }
        }
        public string TitleEn
        {
            get { return _title_en; }
        }
        public string Desc
        {
            get { return _desc; }
        }
        public string DescEn
        {
            get { return _desc_en; }
        }
        public DateTime SubmitDate
        {
            get { return _submitDate; }
        }
        public bool IsVisible
        {
            get { return _isVisible; }
        }
        public DataTable GetAllLinks()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetAllLinks";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
            }
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }

        public DataTable GetVisibleLinks()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetVisibleLinks";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
            }
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }
        public int GetLinkById(int linkId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {

                cm.CommandText = "GetLinkById";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@linkid", SqlDbType.Int);
                cm.Parameters["@linkid"].Value = linkId;
            }

            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _linkId = Convert.ToInt32(dr["linkid"]);
                    _url = Convert.IsDBNull(dr["url"])? "":dr["url"].ToString();
                    _title = Convert.IsDBNull(dr["title"])? "":dr["title"].ToString();
                    _title_en = Convert.IsDBNull(dr["title_en"])? "":dr["title_en"].ToString();
                    _desc = Convert.IsDBNull(dr["desc"])?"":dr["desc"].ToString();
                    _desc_en = Convert.IsDBNull(dr["desc_en"])?"":dr["desc_en"].ToString();
                    _submitDate = Convert.ToDateTime(dr["submitDate"]);
                    _isVisible = dr["isVisible"].ToString() == "1" ? true : false;
                    iRecordsReturned = 1;
                }

                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
        public int UpdateLinks(int linkid, string url, string title, string title_en, string desc, string desc_en, DateTime submitDate, bool isVisible)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {
                cm.CommandText = "UpdateLinks";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                {
                    cm.Parameters.Add("@linkid", SqlDbType.Int);
                    cm.Parameters.Add("@url", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@title", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@title_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@desc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@desc_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@submitDate", SqlDbType.SmallDateTime);
                    cm.Parameters.Add("@isVisible", SqlDbType.TinyInt);
                }
                cm.Parameters["@linkid"].Value = linkid;
                cm.Parameters["@url"].Value = url.Length == 0 ? SqlString.Null : url;
                cm.Parameters["@title"].Value = title.Length == 0 ? SqlString.Null : title;
                cm.Parameters["@title_en"].Value = title_en.Length == 0 ? SqlString.Null : title_en;
                cm.Parameters["@desc"].Value = desc.Length == 0 ? SqlString.Null : desc;
                cm.Parameters["@desc_en"].Value = desc_en.Length == 0 ? SqlString.Null : desc_en;
                cm.Parameters["@submitDate"].Value = submitDate;
                cm.Parameters["@isVisible"].Value = isVisible ? 1 : 0;
            }
            try
            {
                cn.Open();
                iRecordsReturned = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
        public int DeleteLinks(int linkid)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {
                cm.CommandText = "deleteLinks";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@linkid", SqlDbType.Int);
                cm.Parameters["@linkid"].Value = linkid;
            }
            try
            {
                cn.Open();
                iRecordsReturned = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
        public int InsertLink(ref int linkid, string url, string title, string title_en, string desc, string desc_en, DateTime submitDate, bool isVisible)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {
                cm.CommandText = "InsertLink";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                {
                    cm.Parameters.Add("@linkid", SqlDbType.Int);
                    cm.Parameters.Add("@url", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@title", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@title_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@desc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@desc_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@submitDate", SqlDbType.SmallDateTime);
                    cm.Parameters.Add("@isVisible", SqlDbType.TinyInt);
                }
                cm.Parameters["@linkid"].Direction = ParameterDirection.Output;

                cm.Parameters["@linkid"].Value = linkid;
                cm.Parameters["@url"].Value = url.Length == 0 ? SqlString.Null : url;
                cm.Parameters["@title"].Value = title.Length == 0 ? SqlString.Null : title;
                cm.Parameters["@title_en"].Value = title_en.Length == 0 ? SqlString.Null : title_en;
                cm.Parameters["@desc"].Value = desc.Length == 0 ? SqlString.Null : desc;
                cm.Parameters["@desc_en"].Value = desc_en.Length == 0 ? SqlString.Null : desc_en;
                cm.Parameters["@submitDate"].Value = submitDate;
                cm.Parameters["@isVisible"].Value = isVisible ? 1 : 0;
            }
            try
            {
                cn.Open();
                iRecordsReturned = cm.ExecuteNonQuery();
                linkid = int.Parse(cm.Parameters["@linkid"].Value.ToString());

                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
    }

    public class gdsDocuments
    {
        private int _documentid;
        private int _categoryid;
        private string _url;
        private string _title;
        private string _title_en;
        private string _titlealt;
        private string _titlealt_en;
        private string _desc;
        private string _desc_en;
        private DateTime _submitDate;
        private int _size;
        private int _w;
        private int _h;
        private bool _isVisible;
        public int DocumentId
        {
            get { return _documentid; }
        }
        public int CategoryId
        {
            get { return _categoryid; }
        }
        public string Url
        {
            get { return _url; }
        }
        public string Title
        {
            get { return _title; }
        }
        public string TitleEn
        {
            get { return _title_en; }
        }
        public string TitleAlt
        {
            get { return _titlealt; }
        }
        public string TitleAltEn
        {
            get { return _titlealt_en; }
        }
        public string Desc
        {
            get { return _desc; }
        }
        public string DescEn
        {
            get { return _desc_en; }
        }
        public int Size
        {
            get { return _size; }
        }

        public int W
        {
            get { return _w; }
        }

        public int H
        {
            get { return _h; }
        }
        public DateTime SubmitDate
        {
            get { return _submitDate; }
        }
        public bool IsVisible
        {
            get { return _isVisible; }
        }
        public DataTable GetDocuments()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetDocuments";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
            }
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }

        public DataTable GetDocuments(int startRecord, int maxRecords)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            {
                cm.CommandText = "GetDocuments";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
            }
            try
            {
                da.Fill(ds, startRecord, maxRecords, "documents");
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return ds.Tables[0];
        }
        public DataTable GetVisibleDocuments()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetVisibleDocuments";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
            }
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }

            return dt;
        }
        public DataTable GetVisibleDocuments(int startRecord, int maxRecords)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            {
                cm.CommandText = "GetVisibleDocuments";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
            }
            try
            {
                da.Fill(ds, startRecord, maxRecords, "documents");
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return ds.Tables[0];
        }
        public DataTable GetDocumentsByCategoryId(int categoryId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetDocumentsByCategoryId";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@categoryid", SqlDbType.Int);
                cm.Parameters["@categoryid"].Value = categoryId;
            }
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }
        public DataTable GetDocumentsByCategoryId(int categoryId, int startRecord, int maxRecords)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            DataTable dt = null;
            {
                cm.CommandText = "GetDocumentsByCategoryId";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@categoryid", SqlDbType.Int);
                cm.Parameters["@categoryid"].Value = categoryId;
            }
            try
            {
                da.Fill(ds, startRecord, maxRecords, "documents");
                dt = ds.Tables[0];
                return dt;
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }

        public DataTable GetVisibleDocumentsByCategoryId(int categoryId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetVisibleDocumentsByCategoryId";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@categoryid", SqlDbType.Int);
                cm.Parameters["@categoryid"].Value = categoryId;
            }

            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }
        public DataTable GetVisibleDocumentsByCategoryId(int categoryId, int startRecord, int maxRecords)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            {
                cm.CommandText = "GetVisibleDocumentsByCategoryId";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@categoryid", SqlDbType.Int);
                cm.Parameters["@categoryid"].Value = categoryId;
            }

            try
            {
                da.Fill(ds, startRecord, maxRecords, "documents");
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return ds.Tables[0];
        }

        public DataTable GetTop3VisibleDocumentsByCategoryId(int categoryId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetTop3VisibleDocumentsByCategoryId";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@categoryid", SqlDbType.Int);
                cm.Parameters["@categoryid"].Value = categoryId;
            }

            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }
        public int GetDocumentById(int documentId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {
                cm.CommandText = "GetDocumentById";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@documentid", SqlDbType.Int);
                cm.Parameters["@documentid"].Value = documentId;
            }

            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _documentid = int.Parse(dr["documentid"].ToString());
                    _categoryid = int.Parse(dr["categoryid"].ToString());
                    _url = Convert.IsDBNull(dr["url"]) ? "" : dr["url"].ToString();
                    _title = Convert.IsDBNull(dr["title"]) ? "" : dr["title"].ToString();
                    _title_en = Convert.IsDBNull(dr["title_en"]) ? "" : dr["title_en"].ToString();
                    _titlealt = Convert.IsDBNull(dr["titlealt"]) ? "" : dr["titlealt"].ToString();
                    _titlealt_en = Convert.IsDBNull(dr["titlealt_en"]) ? "" : dr["titlealt_en"].ToString();
                    _desc = Convert.IsDBNull(dr["desc"]) ? "" : dr["desc"].ToString();
                    _desc_en = Convert.IsDBNull(dr["desc_en"])?"":dr["desc_en"].ToString();
                    _submitDate = DateTime.Parse(dr["submitDate"].ToString());
                    _size = int.Parse(dr["size"].ToString());
                    _w = int.Parse(dr["w"].ToString());
                    _h = int.Parse(dr["h"].ToString());
                    _isVisible = dr["isVisible"].ToString() == "1" ? true : false;
                    iRecordsReturned = 1;
                }

                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
        public DataTable GetMaxDocuments(int maxRecords = 0, int categoryId = 11, bool isVisible = true)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                cm.CommandText = "GetMaxDocuments";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                {
                    cm.Parameters.Add("@maxrecords", SqlDbType.Int);
                    cm.Parameters.Add("@categoryid", SqlDbType.Int);
                    cm.Parameters.Add("@isvisible", SqlDbType.TinyInt);
                }
                cm.Parameters["@maxrecords"].Value = maxRecords;
                cm.Parameters["@categoryId"].Value = categoryId;
                cm.Parameters["@isVisible"].Value = isVisible ? 1 : 0;
            }
            try
            {
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return dt;
        }

        public int UpdateDocuments(int documentid, int categoryid, string url, string title, string title_en, string titlealt, string titlealt_en, string desc, string desc_en, DateTime submitDate, int size, int w, int h, bool isVisible, bool isChecked)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {
                cm.CommandText = "UpdateDocuments";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                {
                    cm.Parameters.Add("@documentid", SqlDbType.Int);
                    cm.Parameters.Add("@categoryid", SqlDbType.Int);
                    cm.Parameters.Add("@url", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@title", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@title_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@titlealt", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@titlealt_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@desc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@desc_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@submitDate", SqlDbType.SmallDateTime);
                    cm.Parameters.Add("@size", SqlDbType.Int);
                    cm.Parameters.Add("@w", SqlDbType.Int);
                    cm.Parameters.Add("@h", SqlDbType.Int);
                    cm.Parameters.Add("@isVisible", SqlDbType.TinyInt);
                    cm.Parameters.Add("@isChecked", SqlDbType.TinyInt);
                }
                cm.Parameters["@documentid"].Value = documentid;
                cm.Parameters["@categoryid"].Value = categoryid;
                cm.Parameters["@url"].Value = url.Length == 0 ? SqlString.Null : url;
                cm.Parameters["@title"].Value = title.Length == 0 ? SqlString.Null : title;
                cm.Parameters["@title_en"].Value = title_en.Length == 0 ? SqlString.Null : title_en;
                cm.Parameters["@titlealt"].Value = titlealt.Length == 0 ? SqlString.Null : titlealt;
                cm.Parameters["@titlealt_en"].Value = titlealt_en.Length == 0 ? SqlString.Null : titlealt_en;
                cm.Parameters["@desc"].Value = desc.Length == 0 ? SqlString.Null : desc;
                cm.Parameters["@desc_en"].Value = desc_en.Length == 0 ? SqlString.Null : desc_en;
                cm.Parameters["@submitDate"].Value = submitDate;
                cm.Parameters["@size"].Value = size;
                cm.Parameters["@w"].Value = w;
                cm.Parameters["@h"].Value = h;
                cm.Parameters["@isVisible"].Value = isVisible ? 1 : 0;
                cm.Parameters["@isChecked"].Value = isChecked ? 1 : 0;
            }
            try
            {
                cn.Open();
                iRecordsReturned = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
        public int InsertDocument(ref int documentid, int categoryid, string url, string title, string title_en, string titlealt, string titlealt_en, string desc, string desc_en, DateTime submitDate, int size, int w, int h, bool isVisible, bool isChecked)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {
                cm.CommandText = "InsertDocument";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                {
                    cm.Parameters.Add("@documentid", SqlDbType.Int);
                    cm.Parameters.Add("@categoryid", SqlDbType.Int);
                    cm.Parameters.Add("@url", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@title", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@title_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@titlealt", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@titlealt_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@desc", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@desc_en", SqlDbType.VarChar, 255);
                    cm.Parameters.Add("@submitDate", SqlDbType.SmallDateTime);
                    cm.Parameters.Add("@size", SqlDbType.Int);
                    cm.Parameters.Add("@w", SqlDbType.Int);
                    cm.Parameters.Add("@h", SqlDbType.Int);
                    cm.Parameters.Add("@isVisible", SqlDbType.TinyInt);
                    cm.Parameters.Add("@isChecked", SqlDbType.TinyInt);
                }
                cm.Parameters["@documentid"].Direction = ParameterDirection.Output;

                cm.Parameters["@categoryid"].Value = categoryid;
                cm.Parameters["@url"].Value = url.Length == 0 ? SqlString.Null : url;
                cm.Parameters["@title"].Value = title.Length == 0 ? SqlString.Null : title;
                cm.Parameters["@title_en"].Value = title_en.Length == 0 ? SqlString.Null : title_en;
                cm.Parameters["@titlealt"].Value = titlealt.Length == 0 ? SqlString.Null : titlealt;
                cm.Parameters["@titlealt_en"].Value = titlealt_en.Length == 0 ? SqlString.Null : titlealt_en;
                cm.Parameters["@desc"].Value = desc.Length == 0 ? SqlString.Null : desc;
                cm.Parameters["@desc_en"].Value = desc_en.Length == 0 ? SqlString.Null : desc_en;
                cm.Parameters["@submitDate"].Value = submitDate;
                cm.Parameters["@size"].Value = size;
                cm.Parameters["@w"].Value = w;
                cm.Parameters["@h"].Value = h;
                cm.Parameters["@isVisible"].Value = isVisible ? 1 : 0;
                cm.Parameters["@isChecked"].Value = isChecked ? 1 : 0;
            }
            try
            {
                cn.Open();
                iRecordsReturned = cm.ExecuteNonQuery();
                documentid = int.Parse(cm.Parameters["@documentid"].Value.ToString());
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }

            return iRecordsReturned;
        }
        public int DeleteDocument(int documentid)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {
                cm.CommandText = "DeleteDocument";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Connection = cn;
                cm.Parameters.Add("@documentid", SqlDbType.Int);
                cm.Parameters["@documentid"].Value = documentid;
            }
            try
            {
                cn.Open();
                iRecordsReturned = cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException ex)
            {
                commonModule.RedirectError(ex);
            }
            catch (Exception ex)
            {
                commonModule.RedirectError(ex);
            }
            return iRecordsReturned;
        }
    }

    public interface IGdsTree
    {
        int DatasetNumber { get; set; }
        bool IsEnglish { get; set; }
        bool IsSecondLevelVisible { get; set; }

        string DivContentStyle { get; set; }
        string DivContentClass { get; set; }
        string DivContentAdditionalAttr { get; set; }

        string DivTitleStyle { get; set; }
        string DivTitleClass { get; set; }
        string DivTitleAdditionalAttr { get; set; }

        string DivContainerStyle { get; set; }
        string DivContainerClass { get; set; }
        string DivContainerAdditionalAttr { get; set; }

        string DivTitleString { get; set; }
        string DivTitleStringEn { get; set; }
    }

    public enum Language : int
    {
        Indonesian = 0,
        English = 1
    }

}
