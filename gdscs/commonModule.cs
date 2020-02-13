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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.VisualBasic;
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
            System.Web.HttpContext.Current.Session["errMsg"] = "Exception: " + DateTime.Now + Strings.Chr(10) + ex.Message + Strings.Chr(10) + System.Web.HttpContext.Current.Request.Url.ToString() + Strings.Chr(10) + ex.StackTrace;
            System.Web.HttpContext.Current.Response.Redirect("err.aspx");
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
            s = System.Web.HttpContext.Current.Server.HtmlEncode(inputStr.Trim());
            s = s.Replace("'", "''");
            s = s.Replace("\"", "&quot;");
            return s;
        }
        public static string GetDbSafeString(string inputStr)
        {
            string s;
            s = System.Web.HttpContext.Current.Server.HtmlDecode(inputStr.Trim());
            return s;
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
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
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
                var withBlock = HttpContext.Current.Response;
                withBlock.ContentType = "application/vnd.ms-excel";
                withBlock.AddHeader("content-disposition", "inline; filename=" + fileName);
                withBlock.Charset = "";
                withBlock.Write(s);
                withBlock.Flush();
                withBlock.End();
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
                    sResult += Strings.Chr(10);
                    if (sResult.Split(Strings.Chr(10)).Length > 0)
                        sResult = s(sResult.Split(Strings.Chr(10))[1], w, fontSize);
                    break;
                }
            }
            sResult += Strings.Right(input, (input.Length + 1) - j);
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
            BufferCrLf = Strings.Split(Expression, Strings.Chr(10).ToString());
            for (k = 0; k <= Information.UBound(BufferCrLf); k++)
            {
                if (Strings.Len(BufferCrLf[k]) <= Length)
                    Buffer = Buffer + BufferCrLf[k] + Strings.Chr(10);
                else
                {
                    BufferSpace = Strings.Split(BufferCrLf[k], " ");
                    for (j = 0; j <= Information.UBound(BufferSpace); j++)
                    {
                        count += Strings.Len(BufferSpace[j]) + 1;
                        if ((count <= Length))
                            Buffer = Buffer + BufferSpace[j] + " ";
                        else
                        {
                            count = 0;
                            Buffer = Buffer + Strings.Chr(10) + BufferSpace[j] + " ";
                            count = Strings.Len(BufferSpace[j]) + 1;
                        }
                    }
                    Buffer = Buffer + Strings.Chr(10);
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
                    sResult += Strings.Chr(10);
                    break;
                }
            }

            sResult += Strings.Right(text, (text.Length + 1) - ii);
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
            get
            {
                return _varDescEn;
            }
        }

        public string DataSetDesc
        {
            get
            {
                return _dataSetDesc;
            }
        }
        public string DataSetDescEn
        {
            get
            {
                return _dataSetDescEn;
            }
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
                var withBlock = cm;
                withBlock.CommandText = "GetSurveyHighlight";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@gdsid", SqlDbType.TinyInt);
                    withBlock1.Add("@varid", SqlDbType.Int);
                    withBlock1.Add("@gdsdesc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@gdsdesc_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@vardesc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@vardesc_en", SqlDbType.VarChar, 255);
                }
                withBlock.Parameters["@gdsid"].Value = gdsId;
                withBlock.Parameters["@varid"].Value = varId;
                withBlock.Parameters["@gdsdesc"].Value = _dataSetDesc;
                withBlock.Parameters["@gdsdesc_en"].Value = _dataSetDescEn;
                withBlock.Parameters["@vardesc"].Value = _varDesc;
                withBlock.Parameters["@vardesc_en"].Value = _varDescEn;

                withBlock.Parameters["@gdsdesc"].Direction = ParameterDirection.Output;
                withBlock.Parameters["@gdsdesc_en"].Direction = ParameterDirection.Output;
                withBlock.Parameters["@vardesc"].Direction = ParameterDirection.Output;
                withBlock.Parameters["@vardesc_en"].Direction = ParameterDirection.Output;
            }
            try
            {
                da.Fill(ds);

                {
                    var withBlock = cm;
                    _dataSetDesc = withBlock.Parameters["@gdsdesc"].Value.ToString();
                    _dataSetDescEn = withBlock.Parameters["@gdsdesc_en"].Value.ToString();
                    _varDesc = withBlock.Parameters["@vardesc"].Value.ToString();
                    _varDescEn = withBlock.Parameters["@vardesc_en"].Value.ToString();
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
                var withBlock = cm;
                withBlock.CommandText = "GetSurveyHighlightByIsland";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@gdsid", SqlDbType.TinyInt);
                    withBlock1.Add("@varid", SqlDbType.Int);
                    withBlock1.Add("@islandid", SqlDbType.Int);
                    withBlock1.Add("@gdsdesc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@gdsdesc_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@vardesc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@vardesc_en", SqlDbType.VarChar, 255);
                }
                withBlock.Parameters["@gdsid"].Value = gdsId;
                withBlock.Parameters["@varid"].Value = varId;
                withBlock.Parameters["@islandid"].Value = islandId;
                withBlock.Parameters["@gdsdesc"].Value = _dataSetDesc;
                withBlock.Parameters["@gdsdesc_en"].Value = _dataSetDescEn;
                withBlock.Parameters["@vardesc"].Value = _varDesc;
                withBlock.Parameters["@vardesc_en"].Value = _varDescEn;

                withBlock.Parameters["@gdsdesc"].Direction = ParameterDirection.Output;
                withBlock.Parameters["@gdsdesc_en"].Direction = ParameterDirection.Output;
                withBlock.Parameters["@vardesc"].Direction = ParameterDirection.Output;
                withBlock.Parameters["@vardesc_en"].Direction = ParameterDirection.Output;
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
                        drw["desc"] = Interaction.IIf(Information.IsDBNull(dr["desc_en"]), "", dr["desc_en"]);
                    else
                        drw["desc"] = Interaction.IIf(Information.IsDBNull(dr["desc"]), "", dr["desc"]);
                    drw["CountNatl"] = Interaction.IIf(Information.IsDBNull(dr["CountNatl"]), 0, dr["CountNatl"]);
                    dt.Rows.Add(drw);
                }

                cn.Close();
                {
                    var withBlock = cm;
                    _dataSetDesc = withBlock.Parameters["@gdsdesc"].Value.ToString();
                    _dataSetDescEn = withBlock.Parameters["@gdsdesc_en"].Value.ToString();
                    _varDesc = withBlock.Parameters["@vardesc"].Value.ToString();
                    _varDescEn = withBlock.Parameters["@vardesc_en"].Value.ToString();
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
                var withBlock = cm;
                withBlock.CommandText = "GetSurveyHighlight";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@gdsid", SqlDbType.TinyInt);
                    withBlock1.Add("@varid", SqlDbType.Int);
                    withBlock1.Add("@gdsdesc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@gdsdesc_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@vardesc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@vardesc_en", SqlDbType.VarChar, 255);
                }
                withBlock.Parameters["@gdsid"].Value = gdsId;
                withBlock.Parameters["@varid"].Value = varId;
                withBlock.Parameters["@gdsdesc"].Value = _dataSetDesc;
                withBlock.Parameters["@gdsdesc_en"].Value = _dataSetDescEn;
                withBlock.Parameters["@vardesc"].Value = _varDesc;
                withBlock.Parameters["@vardesc_en"].Value = _varDescEn;

                withBlock.Parameters["@gdsdesc"].Direction = ParameterDirection.Output;
                withBlock.Parameters["@gdsdesc_en"].Direction = ParameterDirection.Output;
                withBlock.Parameters["@vardesc"].Direction = ParameterDirection.Output;
                withBlock.Parameters["@vardesc_en"].Direction = ParameterDirection.Output;
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
                        drw["desc"] = Interaction.IIf(Information.IsDBNull(dr["desc_en"]), "", dr["desc_en"]);
                    else
                        drw["desc"] = Interaction.IIf(Information.IsDBNull(dr["desc"]), "", dr["desc"]);
                    drw["CountNatl"] = Interaction.IIf(Information.IsDBNull(dr["CountNatl"]), 0, dr["CountNatl"]);
                    dt.Rows.Add(drw);
                }

                cn.Close();
                {
                    var withBlock = cm;
                    _dataSetDesc = withBlock.Parameters["@gdsdesc"].Value.ToString();
                    _dataSetDescEn = withBlock.Parameters["@gdsdesc_en"].Value.ToString();
                    _varDesc = withBlock.Parameters["@vardesc"].Value.ToString();
                    _varDescEn = withBlock.Parameters["@vardesc_en"].Value.ToString();
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
            string sql = "SELECT     t01prov.prov AS prov , provnm, kabu, kabunm " + " FROM t01prov INNER JOIN " + " t02kabu ON t01prov.prov = t02kabu.prov " + " WHERE t01prov.prov = '" + prov + "' " + Interaction.IIf(kabu != "All", " AND  kabu = '" + kabu + "' ", "");
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand(sql, cn);

            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _prov = Interaction.IIf(Information.IsDBNull(dr["prov"]), "", dr["prov"]).ToString();
                    _provnm = Interaction.IIf(Information.IsDBNull(dr["provnm"]), "", dr["provnm"]).ToString();
                    _kabu = Interaction.IIf(Information.IsDBNull(dr["kabu"]), "", dr["kabu"]).ToString();
                    _kabunm = Interaction.IIf(Information.IsDBNull(dr["kabunm"]), "", dr["kabunm"]).ToString();
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
            get
            {
                return _prov;
            }
        }
        public string ProvName
        {
            get
            {
                return _provnm;
            }
        }
        public string Kabu
        {
            get
            {
                return _kabu;
            }
        }
        public string KabuName
        {
            get
            {
                return _kabunm;
            }
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
        // Sub New(ByVal var_id As Integer)
        // Dim cn As New SqlConnection(commonModule.GetConnString)
        // Dim cm As New SqlCommand(" SELECT * FROM gds2_11_var WHERE var_id = " & var_id, cn)
        // cn.Open()
        // Dim dr As SqlDataReader = cm.ExecuteReader
        // If dr.Read Then
        // _var_id = dr("var_id")
        // _var_parent = dr("var_parent")
        // _var = dr("var")
        // _desc = dr("desc")
        // _desc_en = dr("desc_en")
        // _q = dr("q")
        // _q = dr("q_en")
        // _tbl = dr("tbl")
        // _tbl_id = dr("tbl_id")
        // _lvl = dr("lvl")
        // _isVisible = Boolean.Parse(dr("_isVisible").ToString)
        // _isContVar = Boolean.Parse(dr("isContVar").ToString)
        // End If

        // dr.Close()
        // cn.Close()
        // End Sub

        public gdsVar(int var_id, string varTbl)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand(" SELECT * FROM " + varTbl + " WHERE var_id = " + var_id, cn);

            try
            {
                cn.Open();
                SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _var_id = int.Parse(dr["var_id"].ToString());
                    _var_parent = int.Parse(dr["var_parent"].ToString());
                    _var = dr["var"].ToString();
                    _desc = dr["desc"].ToString();
                    _desc_en = Interaction.IIf(Information.IsDBNull(dr["desc_en"]), _desc, dr["desc_en"]).ToString();
                    _q = Interaction.IIf(Information.IsDBNull(dr["q"]), _desc, dr["q"]).ToString();
                    _q_en = Interaction.IIf(Information.IsDBNull(dr["q_en"]), _desc_en, dr["q_en"]).ToString();
                    _tbl = dr["tbl"].ToString();
                    _tbl_id = dr["tbl_id"].ToString();
                    _lvl = int.Parse(dr["lvl"].ToString());
                    _isVisible = System.Convert.ToBoolean(dr["isVisible"]);
                    _isContVar = System.Convert.ToBoolean(dr["isContVar"]);
                    _isAdvance = System.Convert.ToBoolean(dr["isAdvance"]);
                    _criteria = Interaction.IIf(Information.IsDBNull(dr["criteria"]), "", dr["criteria"]).ToString();
                }
                cm.CommandText = "SELECT * FROM " + varTbl + " WHERE var_id =" + Interaction.IIf(Information.IsDBNull(dr["var_parent"]), "", _var_parent);
                dr.Close();
                dr = cm.ExecuteReader();

                if (dr.Read())
                {
                    _var_parent_desc = dr["desc"].ToString();
                    _var_parent_desc_en = Interaction.IIf(Information.IsDBNull(dr["desc_en"]), _var_parent_desc, dr["desc_en"]).ToString();
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

        public int Var_Id
        {
            get
            {
                return _var_id;
            }
        }
        public int Var_Parent
        {
            get
            {
                return _var_parent;
            }
        }
        public string Var_Parent_Desc
        {
            get
            {
                return _var_parent_desc;
            }
        }
        public string Var_Parent_Desc_En
        {
            get
            {
                return _var_parent_desc_en;
            }
        }
        public int Ord
        {
            get
            {
                return _ord;
            }
        }
        public string Var
        {
            get
            {
                return _var;
            }
        }
        public string Desc
        {
            get
            {
                return _desc;
            }
        }
        public string Desc_En
        {
            get
            {
                return _desc_en;
            }
        }
        public string Q
        {
            get
            {
                return _q;
            }
        }
        public string Q_En
        {
            get
            {
                return _q_en;
            }
        }
        public string Tbl
        {
            get
            {
                return _tbl;
            }
        }
        public string Tbl_Id
        {
            get
            {
                return _tbl_id;
            }
        }
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
        }
        public bool IsContVar
        {
            get
            {
                return _isContVar;
            }
        }
        public bool IsAdvance
        {
            get
            {
                return _isAdvance;
            }
        }
        public string Criteria
        {
            get
            {
                return _criteria;
            }
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


        // Sub New()
        // Dim cn As New SqlConnection(commonModule.GetConnString)
        // Dim cm As New SqlCommand(" SELECT * FROM gds2 WHERE gds_id = 11 ", cn)
        // cn.Open()
        // Dim dr As SqlDataReader = cm.ExecuteReader
        // If dr.Read Then
        // _gdsId = dr("gds_id")
        // _varTable = dr("vartbl")
        // _valueTable = dr("valuetbl")
        // _avTable = dr("avtbl")
        // _compTable = dr("comptbl")
        // _desc = dr("desc")
        // _desc_En = dr("desc_en")
        // _kabufld = dr("kabufld")
        // _kabunmfld = dr("kabunmfld")
        // _provfld = dr("provfld")
        // _provnmfld = dr("provnmfld")
        // _baseTable = dr("baseTbl")
        // End If
        // dr.Close()
        // cn.Close()
        // End Sub
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
            get
            {
                return _gdsId;
            }
        }
        public string VarTable
        {
            get
            {
                return _varTable;
            }
        }
        public string ValueTable
        {
            get
            {
                return _valueTable;
            }
        }
        public string AvTable
        {
            get
            {
                return _avTable;
            }
        }
        public string CompTable
        {
            get
            {
                return _compTable;
            }
        }
        public string Desc
        {
            get
            {
                return _desc;
            }
        }
        public string Desc_En
        {
            get
            {
                return _desc_En;
            }
        }
        public string ProvFld
        {
            get
            {
                return _provfld;
            }
        }
        public string ProvNmFld
        {
            get
            {
                return _provnmfld;
            }
        }
        public string KabuFld
        {
            get
            {
                return _kabufld;
            }
        }
        public string KabuNmFld
        {
            get
            {
                return _kabunmfld;
            }
        }
        public string BaseTable
        {
            get
            {
                return _baseTable;
            }
        }
        public bool HasKeca
        {
            get
            {
                return _hasKeca;
            }
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
            get
            {
                return _Title;
            }
        }
        public string TitleEn
        {
            get
            {
                return _TitleEn;
            }
        }

        public string Content
        {
            get
            {
                return _Content;
            }
        }
        public string ContentEn
        {
            get
            {
                return _ContentEn;
            }
        }
        public int panelId
        {
            get
            {
                return _panelId;
            }
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            {
                var withBlock = cm;
                withBlock.CommandText = "GetContentEn";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                withBlock.Parameters.Add("@panelid", SqlDbType.Int);
                withBlock.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                System.Data.SqlClient.SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Title = Interaction.IIf(Information.IsDBNull(dr["title_en"]), "", dr["title_en"]).ToString();
                    _Content = Interaction.IIf(Information.IsDBNull(dr["content_en"]), "", dr["content_en"]).ToString();
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            {
                var withBlock = cm;
                withBlock.CommandText = "GetContentIna";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                withBlock.Parameters.Add("@panelid", SqlDbType.Int);
                withBlock.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                System.Data.SqlClient.SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Title = Interaction.IIf(Information.IsDBNull(dr["title"]), "", dr["title"]).ToString();
                    _Content = Interaction.IIf(Information.IsDBNull(dr["content"]), "", dr["content"]).ToString();
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "UpdateContent";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@panelid", SqlDbType.Int);
                    withBlock1.Add("@title", SqlDbType.VarChar, 50);
                    withBlock1.Add("@title_en", SqlDbType.VarChar, 50);
                    withBlock1.Add("@content", SqlDbType.Text);
                    withBlock1.Add("@content_en", SqlDbType.Text);
                }

                withBlock.Parameters["@panelid"].Value = panelId;
                withBlock.Parameters["@title"].Value = Interaction.IIf(title.Length == 0, System.Data.SqlTypes.SqlString.Null, title);
                withBlock.Parameters["@title_en"].Value = Interaction.IIf(title_en.Length == 0, System.Data.SqlTypes.SqlString.Null, title_en);
                withBlock.Parameters["@content"].Value = Interaction.IIf(content.Length == 0, System.Data.SqlTypes.SqlString.Null, content);
                withBlock.Parameters["@content_en"].Value = Interaction.IIf(content_en.Length == 0, System.Data.SqlTypes.SqlString.Null, content_en);
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsReturned = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "GetContent";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                withBlock.Parameters.Add("@panelid", SqlDbType.Int);
                withBlock.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                System.Data.SqlClient.SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Title = Interaction.IIf(Information.IsDBNull(dr["title"]), "", dr["title"]).ToString();
                    _TitleEn = Interaction.IIf(Information.IsDBNull(dr["title_en"]), "", dr["title_en"]).ToString();
                    _Content = Interaction.IIf(Information.IsDBNull(dr["content"]), "", dr["content"]).ToString();
                    _ContentEn = Interaction.IIf(Information.IsDBNull(dr["content_en"]), "", dr["content_en"]).ToString();
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
                var withBlock = cm;
                withBlock.CommandText = "GetMaxComments";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@maxrecords", SqlDbType.Int);
                    withBlock1.Add("@isvisible", SqlDbType.TinyInt);
                }
                withBlock.Parameters["@maxrecords"].Value = maxRecords;
                withBlock.Parameters["@isVisible"].Value = isVisible;
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            DataSet ds = new DataSet();
            {
                var withBlock = cm;
                withBlock.CommandText = "GetTop5Comments";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            DataSet ds = new DataSet();
            {
                var withBlock = cm;
                withBlock.CommandText = "GetAllComments";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "InsertComment";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@sender", SqlDbType.VarChar, 50);
                    withBlock1.Add("@email", SqlDbType.VarChar, 255);
                    withBlock1.Add("@url", SqlDbType.VarChar, 255);
                    withBlock1.Add("@comment", SqlDbType.VarChar, 255);
                    withBlock1.Add("@submitdate", SqlDbType.SmallDateTime);
                    withBlock1.Add("@isvisible", SqlDbType.TinyInt);
                }
                withBlock.Parameters["@sender"].Value = Interaction.IIf(sender.Length == 0, System.Data.SqlTypes.SqlString.Null, sender);
                withBlock.Parameters["@email"].Value = Interaction.IIf(email.Length == 0, System.Data.SqlTypes.SqlString.Null, email);
                withBlock.Parameters["@url"].Value = Interaction.IIf(url.Length == 0, System.Data.SqlTypes.SqlString.Null, url);
                withBlock.Parameters["@comment"].Value = Interaction.IIf(comment.Length == 0, System.Data.SqlTypes.SqlString.Null, comment);
                withBlock.Parameters["@isvisible"].Value = isvisible;
                withBlock.Parameters["@submitdate"].Value = submitdate;
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "UpdateComment";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@commentid", SqlDbType.Int);
                    withBlock1.Add("@sender", SqlDbType.VarChar, 50);
                    withBlock1.Add("@email", SqlDbType.VarChar, 255);
                    withBlock1.Add("@url", SqlDbType.VarChar, 255);
                    withBlock1.Add("@comment", SqlDbType.VarChar, 255);
                    withBlock1.Add("@submitdate", SqlDbType.SmallDateTime);
                    withBlock1.Add("@isvisible", SqlDbType.TinyInt);
                }
                withBlock.Parameters["@commentid"].Value = commentid;
                withBlock.Parameters["@sender"].Value = Interaction.IIf(sender.Length == 0, System.Data.SqlTypes.SqlString.Null, sender);
                withBlock.Parameters["@email"].Value = Interaction.IIf(email.Length == 0, System.Data.SqlTypes.SqlString.Null, email);
                withBlock.Parameters["@url"].Value = Interaction.IIf(url.Length == 0, System.Data.SqlTypes.SqlString.Null, url);
                withBlock.Parameters["@comment"].Value = Interaction.IIf(comment.Length == 0, System.Data.SqlTypes.SqlString.Null, comment);
                withBlock.Parameters["@isvisible"].Value = isvisible;
                withBlock.Parameters["@submitdate"].Value = submitdate;
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "DeleteComment";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@commentid", SqlDbType.Int);
                withBlock.Parameters["@commentid"].Value = commentId;
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "GetGenericContentByIdLang";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                withBlock.Parameters.Add("@panelid", SqlDbType.Int);
                withBlock.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                System.Data.SqlClient.SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Content = Interaction.IIf(Information.IsDBNull(dr["generictext"]), "", dr["generictext"]).ToString();
                    _ContentEn = Interaction.IIf(Information.IsDBNull(dr["generictext_en"]), "", dr["generictext_en"]).ToString();
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;

            {
                var withBlock = cm;
                withBlock.CommandText = "GetGenericContentById";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                withBlock.Parameters.Add("@panelid", SqlDbType.Int);
                withBlock.Parameters.Add("@en", SqlDbType.TinyInt);
                withBlock.Parameters["@panelid"].Value = panelId;
                withBlock.Parameters["@en"].Value = inEnglish ? 1 : 0;
            }
            try
            {
                cn.Open();
                System.Data.SqlClient.SqlDataReader dr = cm.ExecuteReader();
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "UpdateGenericContentById";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@panelid", SqlDbType.Int);
                    withBlock1.Add("@generictext", SqlDbType.Text);
                    withBlock1.Add("@generictext_en", SqlDbType.Text);
                }
                withBlock.Parameters["@panelid"].Value = panelId;
                withBlock.Parameters["@generictext"].Value = Interaction.IIf(genericText.Length == 0, System.Data.SqlTypes.SqlString.Null, genericText);
                withBlock.Parameters["@generictext_en"].Value = Interaction.IIf(genericTextEn.Length == 0, System.Data.SqlTypes.SqlString.Null, genericTextEn);
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "GetGenericTitleByIdLang";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                withBlock.Parameters.Add("@panelid", SqlDbType.Int);
                withBlock.Parameters["@panelid"].Value = panelId;
            }
            try
            {
                cn.Open();
                System.Data.SqlClient.SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _Title = Interaction.IIf(Information.IsDBNull(dr["generictitle"]), "", dr["generictitle"]).ToString();
                    _TitleEn = Interaction.IIf(Information.IsDBNull(dr["generictitle_en"]), "", dr["generictitle_en"]).ToString();
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;

            {
                var withBlock = cm;
                withBlock.CommandText = "GetGenericTitleById";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                withBlock.Parameters.Add("@panelid", SqlDbType.Int);
                withBlock.Parameters.Add("@en", SqlDbType.TinyInt);
                withBlock.Parameters["@panelid"].Value = panelId;
                withBlock.Parameters["@en"].Value = Interaction.IIf(inEnglish, 1, 0);
            }

            try
            {
                cn.Open();
                System.Data.SqlClient.SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    if (inEnglish)
                        _Content = Interaction.IIf(Information.IsDBNull(dr["generictitle_en"]), "", dr["generictitle_en"]).ToString();
                    else
                        _Content = Interaction.IIf(Information.IsDBNull(dr["generictitle"]), "", dr["generictitle"]).ToString();
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsAffected = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "UpdateGenericTitleById";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@panelid", SqlDbType.Int);
                    withBlock1.Add("@generictitle", SqlDbType.VarChar, 50);
                    withBlock1.Add("@generictitle_en", SqlDbType.VarChar, 50);
                }
                withBlock.Parameters["@panelid"].Value = panelId;
                withBlock.Parameters["@generictitle"].Value = Interaction.IIf(genericTitle.Length == 0, System.Data.SqlTypes.SqlString.Null, genericTitle);
                withBlock.Parameters["@generictitle_en"].Value = Interaction.IIf(genericTitleEn.Length == 0, System.Data.SqlTypes.SqlString.Null, genericTitleEn);
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
            get
            {
                return _desc;
            }
        }

        public string DescEn
        {
            get
            {
                return _desc_en;
            }
        }
        public string Var
        {
            get
            {
                return _var;
            }
        }
        public int GdsId
        {
            get
            {
                return _gds_id;
            }
        }
        public int VarId
        {
            get
            {
                return _var_id;
            }
        }

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
        }

        public bool IsContVar
        {
            get
            {
                return _isContVar;
            }
        }
        public bool IsAdvance
        {
            get
            {
                return _isAdvance;
            }
        }

        public string Q
        {
            get
            {
                return _q;
            }
        }
        public string QEn
        {
            get
            {
                return _q_en;
            }
        }
        public int getVarByVarId(int gdsId, int varId)
        {
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsReturned = 0;

            cm.CommandText = "getVarByVarId";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Connection = cn;

            System.Data.SqlClient.SqlParameter paramGds_Id = new System.Data.SqlClient.SqlParameter("@gds_id", SqlDbType.TinyInt);
            System.Data.SqlClient.SqlParameter paramVar_Id = new System.Data.SqlClient.SqlParameter("@var_id", SqlDbType.Int);

            paramGds_Id.Value = gdsId;
            paramVar_Id.Value = varId;

            cm.Parameters.Add(paramGds_Id);
            cm.Parameters.Add(paramVar_Id);
            try
            {
                cn.Open();
                System.Data.SqlClient.SqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _gds_id = gdsId;
                    _var_id = varId;
                    _desc = commonModule.GetDbSafeString(Interaction.IIf(Information.IsDBNull(dr["desc"]), "", dr["desc"]).ToString());
                    _desc_en = commonModule.GetDbSafeString(Interaction.IIf(Information.IsDBNull(dr["desc_en"]), "", dr["desc_en"]).ToString());
                    _q = commonModule.GetDbSafeString(Interaction.IIf(Information.IsDBNull(dr["q"]), "", dr["q"]).ToString());
                    _q_en = commonModule.GetDbSafeString(Interaction.IIf(Information.IsDBNull(dr["q_en"]), "", dr["q_en"]).ToString());
                    _var = commonModule.GetDbSafeString(Interaction.IIf(Information.IsDBNull(dr["var"]), "", dr["var"]).ToString());
                    _isVisible = dr["isVisible"] == "1" ? true : false;
                    _isContVar = dr["isContVar"] == "1" ? true : false;
                    _isAdvance = dr["isAdvance"] == "1" ? true : false;
                    // If dr("isVisible") = 1 Then
                    // Me.chkIsVisible.Checked = True
                    // End If
                    // If dr("isContVar") = 1 Then
                    // Me.chkIsContVar.Checked = True
                    // End If
                    // If dr("isAdvance") = 1 Then
                    // Me.chkIsAdvance.Checked = True
                    // End If
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();
            int iRecordsReturned = 0;
            {
                var withBlock = cm;
                withBlock.CommandText = "updateVar";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;

                withBlock.Parameters.Add("@gds_id", SqlDbType.TinyInt);
                withBlock.Parameters.Add("@var_id", SqlDbType.Int);
                withBlock.Parameters.Add("@desc", SqlDbType.VarChar, 255);
                withBlock.Parameters.Add("@desc_en", SqlDbType.VarChar, 255);
                withBlock.Parameters.Add("@q", SqlDbType.Text);
                withBlock.Parameters.Add("@q_en", SqlDbType.Text);
                withBlock.Parameters.Add("@var", SqlDbType.VarChar, 50);
                withBlock.Parameters.Add("@isVisible", SqlDbType.TinyInt);
                withBlock.Parameters.Add("@isContVar", SqlDbType.TinyInt);
                withBlock.Parameters.Add("@isAdvance", SqlDbType.TinyInt);

                withBlock.Parameters["@gds_id"].Value = gds_id;
                withBlock.Parameters["@var_id"].Value = var_id;
                withBlock.Parameters["@desc"].Value = Interaction.IIf(desc.Length == 0, System.Data.SqlTypes.SqlString.Null, desc);
                withBlock.Parameters["@desc_en"].Value = Interaction.IIf(desc_en.Length == 0, System.Data.SqlTypes.SqlString.Null, desc_en);
                withBlock.Parameters["@q"].Value = Interaction.IIf(q.Length == 0, System.Data.SqlTypes.SqlString.Null, q);
                withBlock.Parameters["@q_en"].Value = Interaction.IIf(q_en.Length == 0, System.Data.SqlTypes.SqlString.Null, q_en);
                withBlock.Parameters["@var"].Value = Interaction.IIf(var.Length == 0, System.Data.SqlTypes.SqlString.Null, var);
                withBlock.Parameters["@isVisible"].Value = Interaction.IIf(isVisible, 1, 0);
                withBlock.Parameters["@isContVar"].Value = Interaction.IIf(isContVar, 1, 0);
                withBlock.Parameters["@isAdvance"].Value = Interaction.IIf(isAdvance, 1, 0);
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
            System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(commonModule.GetConnString());
            System.Data.SqlClient.SqlCommand cm = new System.Data.SqlClient.SqlCommand();

            cm.CommandText = "getVariablesByGdsId";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Connection = cn;
            System.Data.SqlClient.SqlParameter paramGds_id = new System.Data.SqlClient.SqlParameter("@gds_id", SqlDbType.TinyInt);
            paramGds_id.Value = gdsId;
            cm.Parameters.Add(paramGds_id);
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cm);
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
            get
            {
                return _categoryid;
            }
        }
        public string Desc
        {
            get
            {
                return _desc;
            }
        }
        public string DescEn
        {
            get
            {
                return _desc_en;
            }
        }
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
        }
        public int GetCategoryById(int categoryId)
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            int iRecordsReturned = 0;
            SqlDataReader dr;
            {
                var withBlock = cm;
                withBlock.CommandText = "GetCategoryById";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@categoryid", SqlDbType.Int);
                withBlock.Parameters["@categoryid"].Value = categoryId;
            }
            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _categoryid = Convert.ToInt32(dr["categoryid"]);
                    _desc = Interaction.IIf(Information.IsDBNull(dr["desc"]), "", dr["desc"]).ToString();
                    _desc_en = Interaction.IIf(Information.IsDBNull(dr["desc_en"]), "", dr["desc_en"]).ToString();
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
                var withBlock = cm;
                withBlock.CommandText = "GetCategories";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
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
            get
            {
                return _linkId;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
        }
        public string TitleEn
        {
            get
            {
                return _title_en;
            }
        }
        public string Desc
        {
            get
            {
                return _desc;
            }
        }
        public string DescEn
        {
            get
            {
                return _desc_en;
            }
        }
        public DateTime SubmitDate
        {
            get
            {
                return _submitDate;
            }
        }
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
        }
        public DataTable GetAllLinks()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                var withBlock = cm;
                withBlock.CommandText = "GetAllLinks";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
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
                var withBlock = cm;
                withBlock.CommandText = "GetVisibleLinks";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
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
                var withBlock = cm;
                withBlock.CommandText = "GetLinkById";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@linkid", SqlDbType.Int);
                withBlock.Parameters["@linkid"].Value = linkId;
            }

            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _linkId = Convert.ToInt32(dr["linkid"]);
                    _url = Interaction.IIf(Information.IsDBNull(dr["url"]), "", dr["url"]).ToString();
                    _title = Interaction.IIf(Information.IsDBNull(dr["title"]), "", dr["title"]).ToString();
                    _title_en = Interaction.IIf(Information.IsDBNull(dr["title_en"]), "", dr["title_en"]).ToString();
                    _desc = Interaction.IIf(Information.IsDBNull(dr["desc"]), "", dr["desc"]).ToString();
                    _desc_en = Interaction.IIf(Information.IsDBNull(dr["desc_en"]), "", dr["desc_en"]).ToString();
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
                var withBlock = cm;
                withBlock.CommandText = "UpdateLinks";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@linkid", SqlDbType.Int);
                    withBlock1.Add("@url", SqlDbType.VarChar, 255);
                    withBlock1.Add("@title", SqlDbType.VarChar, 255);
                    withBlock1.Add("@title_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@desc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@desc_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@submitDate", SqlDbType.SmallDateTime);
                    withBlock1.Add("@isVisible", SqlDbType.TinyInt);
                }
                withBlock.Parameters["@linkid"].Value = linkid;
                withBlock.Parameters["@url"].Value = Interaction.IIf(url.Length == 0, System.Data.SqlTypes.SqlString.Null, url);
                withBlock.Parameters["@title"].Value = Interaction.IIf(title.Length == 0, System.Data.SqlTypes.SqlString.Null, title);
                withBlock.Parameters["@title_en"].Value = Interaction.IIf(title_en.Length == 0, System.Data.SqlTypes.SqlString.Null, title_en);
                withBlock.Parameters["@desc"].Value = Interaction.IIf(desc.Length == 0, System.Data.SqlTypes.SqlString.Null, desc);
                withBlock.Parameters["@desc_en"].Value = Interaction.IIf(desc_en.Length == 0, System.Data.SqlTypes.SqlString.Null, desc_en);
                withBlock.Parameters["@submitDate"].Value = submitDate;
                withBlock.Parameters["@isVisible"].Value = Interaction.IIf(isVisible, 1, 0);
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
                var withBlock = cm;
                withBlock.CommandText = "deleteLinks";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@linkid", SqlDbType.Int);
                withBlock.Parameters["@linkid"].Value = linkid;
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
                var withBlock = cm;
                withBlock.CommandText = "InsertLink";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@linkid", SqlDbType.Int);
                    withBlock1.Add("@url", SqlDbType.VarChar, 255);
                    withBlock1.Add("@title", SqlDbType.VarChar, 255);
                    withBlock1.Add("@title_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@desc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@desc_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@submitDate", SqlDbType.SmallDateTime);
                    withBlock1.Add("@isVisible", SqlDbType.TinyInt);
                }
                withBlock.Parameters["@linkid"].Direction = ParameterDirection.Output;

                withBlock.Parameters["@linkid"].Value = linkid;
                withBlock.Parameters["@url"].Value = Interaction.IIf(url.Length == 0, System.Data.SqlTypes.SqlString.Null, url);
                withBlock.Parameters["@title"].Value = Interaction.IIf(title.Length == 0, System.Data.SqlTypes.SqlString.Null, title);
                withBlock.Parameters["@title_en"].Value = Interaction.IIf(title_en.Length == 0, System.Data.SqlTypes.SqlString.Null, title_en);
                withBlock.Parameters["@desc"].Value = Interaction.IIf(desc.Length == 0, System.Data.SqlTypes.SqlString.Null, desc);
                withBlock.Parameters["@desc_en"].Value = Interaction.IIf(desc_en.Length == 0, System.Data.SqlTypes.SqlString.Null, desc_en);
                withBlock.Parameters["@submitDate"].Value = submitDate;
                withBlock.Parameters["@isVisible"].Value = Interaction.IIf(isVisible, 1, 0);
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
            get
            {
                return _documentid;
            }
        }
        public int CategoryId
        {
            get
            {
                return _categoryid;
            }
        }
        public string Url
        {
            get
            {
                return _url;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
        }
        public string TitleEn
        {
            get
            {
                return _title_en;
            }
        }
        public string TitleAlt
        {
            get
            {
                return _titlealt;
            }
        }
        public string TitleAltEn
        {
            get
            {
                return _titlealt_en;
            }
        }
        public string Desc
        {
            get
            {
                return _desc;
            }
        }
        public string DescEn
        {
            get
            {
                return _desc_en;
            }
        }
        public int Size
        {
            get
            {
                return _size;
            }
        }

        public int W
        {
            get
            {
                return _w;
            }
        }

        public int H
        {
            get
            {
                return _h;
            }
        }
        public DateTime SubmitDate
        {
            get
            {
                return _submitDate;
            }
        }
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
        }
        public DataTable GetDocuments()
        {
            SqlConnection cn = new SqlConnection(commonModule.GetConnString());
            SqlCommand cm = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            {
                var withBlock = cm;
                withBlock.CommandText = "GetDocuments";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
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
                var withBlock = cm;
                withBlock.CommandText = "GetDocuments";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
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
                var withBlock = cm;
                withBlock.CommandText = "GetVisibleDocuments";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
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
                var withBlock = cm;
                withBlock.CommandText = "GetVisibleDocuments";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
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
                var withBlock = cm;
                withBlock.CommandText = "GetDocumentsByCategoryId";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@categoryid", SqlDbType.Int);
                withBlock.Parameters["@categoryid"].Value = categoryId;
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
                var withBlock = cm;
                withBlock.CommandText = "GetDocumentsByCategoryId";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@categoryid", SqlDbType.Int);
                withBlock.Parameters["@categoryid"].Value = categoryId;
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
                var withBlock = cm;
                withBlock.CommandText = "GetVisibleDocumentsByCategoryId";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@categoryid", SqlDbType.Int);
                withBlock.Parameters["@categoryid"].Value = categoryId;
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
                var withBlock = cm;
                withBlock.CommandText = "GetVisibleDocumentsByCategoryId";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@categoryid", SqlDbType.Int);
                withBlock.Parameters["@categoryid"].Value = categoryId;
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
                var withBlock = cm;
                withBlock.CommandText = "GetTop3VisibleDocumentsByCategoryId";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@categoryid", SqlDbType.Int);
                withBlock.Parameters["@categoryid"].Value = categoryId;
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
                var withBlock = cm;
                withBlock.CommandText = "GetDocumentById";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@documentid", SqlDbType.Int);
                withBlock.Parameters["@documentid"].Value = documentId;
            }

            try
            {
                cn.Open();
                dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    _documentid = int.Parse(dr["documentid"].ToString());
                    _categoryid = int.Parse(dr["categoryid"].ToString());
                    _url = Interaction.IIf(Information.IsDBNull(dr["url"]), "", dr["url"]).ToString();
                    _title = Interaction.IIf(Information.IsDBNull(dr["title"]), "", dr["title"]).ToString();
                    _title_en = Interaction.IIf(Information.IsDBNull(dr["title_en"]), "", dr["title_en"]).ToString();
                    _titlealt = Interaction.IIf(Information.IsDBNull(dr["titlealt"]), "", dr["titlealt"]).ToString();
                    _titlealt_en = Interaction.IIf(Information.IsDBNull(dr["titlealt_en"]), "", dr["titlealt_en"]).ToString();
                    _desc = Interaction.IIf(Information.IsDBNull(dr["desc"]), "", dr["desc"]).ToString();
                    _desc_en = Interaction.IIf(Information.IsDBNull(dr["desc_en"]), "", dr["desc_en"]).ToString();
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
                var withBlock = cm;
                withBlock.CommandText = "GetMaxDocuments";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@maxrecords", SqlDbType.Int);
                    withBlock1.Add("@categoryid", SqlDbType.Int);
                    withBlock1.Add("@isvisible", SqlDbType.TinyInt);
                }
                withBlock.Parameters["@maxrecords"].Value = maxRecords;
                withBlock.Parameters["@categoryId"].Value = categoryId;
                withBlock.Parameters["@isVisible"].Value = Interaction.IIf(isVisible, 1, 0);
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
                var withBlock = cm;
                withBlock.CommandText = "UpdateDocuments";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@documentid", SqlDbType.Int);
                    withBlock1.Add("@categoryid", SqlDbType.Int);
                    withBlock1.Add("@url", SqlDbType.VarChar, 255);
                    withBlock1.Add("@title", SqlDbType.VarChar, 255);
                    withBlock1.Add("@title_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@titlealt", SqlDbType.VarChar, 255);
                    withBlock1.Add("@titlealt_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@desc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@desc_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@submitDate", SqlDbType.SmallDateTime);
                    withBlock1.Add("@size", SqlDbType.Int);
                    withBlock1.Add("@w", SqlDbType.Int);
                    withBlock1.Add("@h", SqlDbType.Int);
                    withBlock1.Add("@isVisible", SqlDbType.TinyInt);
                    withBlock1.Add("@isChecked", SqlDbType.TinyInt);
                }
                withBlock.Parameters["@documentid"].Value = documentid;
                withBlock.Parameters["@categoryid"].Value = categoryid;
                withBlock.Parameters["@url"].Value = Interaction.IIf(url.Length == 0, System.Data.SqlTypes.SqlString.Null, url);
                withBlock.Parameters["@title"].Value = Interaction.IIf(title.Length == 0, System.Data.SqlTypes.SqlString.Null, title);
                withBlock.Parameters["@title_en"].Value = Interaction.IIf(title_en.Length == 0, System.Data.SqlTypes.SqlString.Null, title_en);
                withBlock.Parameters["@titlealt"].Value = Interaction.IIf(titlealt.Length == 0, System.Data.SqlTypes.SqlString.Null, titlealt);
                withBlock.Parameters["@titlealt_en"].Value = Interaction.IIf(titlealt_en.Length == 0, System.Data.SqlTypes.SqlString.Null, titlealt_en);
                withBlock.Parameters["@desc"].Value = Interaction.IIf(desc.Length == 0, System.Data.SqlTypes.SqlString.Null, desc);
                withBlock.Parameters["@desc_en"].Value = Interaction.IIf(desc_en.Length == 0, System.Data.SqlTypes.SqlString.Null, desc_en);
                withBlock.Parameters["@submitDate"].Value = submitDate;
                withBlock.Parameters["@size"].Value = size;
                withBlock.Parameters["@w"].Value = w;
                withBlock.Parameters["@h"].Value = h;
                withBlock.Parameters["@isVisible"].Value = Interaction.IIf(isVisible, 1, 0);
                withBlock.Parameters["@isChecked"].Value = Interaction.IIf(isChecked, 1, 0);
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
                var withBlock = cm;
                withBlock.CommandText = "InsertDocument";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                {
                    var withBlock1 = withBlock.Parameters;
                    withBlock1.Add("@documentid", SqlDbType.Int);
                    withBlock1.Add("@categoryid", SqlDbType.Int);
                    withBlock1.Add("@url", SqlDbType.VarChar, 255);
                    withBlock1.Add("@title", SqlDbType.VarChar, 255);
                    withBlock1.Add("@title_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@titlealt", SqlDbType.VarChar, 255);
                    withBlock1.Add("@titlealt_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@desc", SqlDbType.VarChar, 255);
                    withBlock1.Add("@desc_en", SqlDbType.VarChar, 255);
                    withBlock1.Add("@submitDate", SqlDbType.SmallDateTime);
                    withBlock1.Add("@size", SqlDbType.Int);
                    withBlock1.Add("@w", SqlDbType.Int);
                    withBlock1.Add("@h", SqlDbType.Int);
                    withBlock1.Add("@isVisible", SqlDbType.TinyInt);
                    withBlock1.Add("@isChecked", SqlDbType.TinyInt);
                }
                withBlock.Parameters["@documentid"].Direction = ParameterDirection.Output;

                withBlock.Parameters["@categoryid"].Value = categoryid;
                withBlock.Parameters["@url"].Value = Interaction.IIf(url.Length == 0, System.Data.SqlTypes.SqlString.Null, url);
                withBlock.Parameters["@title"].Value = Interaction.IIf(title.Length == 0, System.Data.SqlTypes.SqlString.Null, title);
                withBlock.Parameters["@title_en"].Value = Interaction.IIf(title_en.Length == 0, System.Data.SqlTypes.SqlString.Null, title_en);
                withBlock.Parameters["@titlealt"].Value = Interaction.IIf(titlealt.Length == 0, System.Data.SqlTypes.SqlString.Null, titlealt);
                withBlock.Parameters["@titlealt_en"].Value = Interaction.IIf(titlealt_en.Length == 0, System.Data.SqlTypes.SqlString.Null, titlealt_en);
                withBlock.Parameters["@desc"].Value = Interaction.IIf(desc.Length == 0, System.Data.SqlTypes.SqlString.Null, desc);
                withBlock.Parameters["@desc_en"].Value = Interaction.IIf(desc_en.Length == 0, System.Data.SqlTypes.SqlString.Null, desc_en);
                withBlock.Parameters["@submitDate"].Value = submitDate;
                withBlock.Parameters["@size"].Value = size;
                withBlock.Parameters["@w"].Value = w;
                withBlock.Parameters["@h"].Value = h;
                withBlock.Parameters["@isVisible"].Value = Interaction.IIf(isVisible, 1, 0);
                withBlock.Parameters["@isChecked"].Value = Interaction.IIf(isChecked, 1, 0);
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
                var withBlock = cm;
                withBlock.CommandText = "DeleteDocument";
                withBlock.CommandType = CommandType.StoredProcedure;
                withBlock.Connection = cn;
                withBlock.Parameters.Add("@documentid", SqlDbType.Int);
                withBlock.Parameters["@documentid"].Value = documentid;
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
