using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// IO Online 公共继承窗体
/// </summary>
public partial class IOForm : System.Web.UI.Page
{
    #region 属性定义
    public const string ExpireDate = "9999-12-31 23:59:59.997";
    //add by andre @ 08/22/16
    const string IOC_LOGGED_IN_COOKIE = "ioc_loggedin";
    const string Base_API_URI = "http://localhost:8005/";
    //add by andre end

    /// <summary>
    /// 数据库操作对象
    /// </summary>
    public DataBase IOBase = new DataBase();
    internal WebUser _User = new WebUser();
    internal bool _Overtime = true;

    /// <summary>
    /// 登录是否超时
    /// </summary>
    public bool Overtime
    {
        get { return _Overtime; }
        set { _Overtime = value; }
    }

    /// <summary>
    /// 当前登录用户
    /// </summary>
    public WebUser CurrentUser
    {
        get { return _User; }
        set { _User = value; }
    }

    internal object _FirstId = null, _OtherIndex = null, _BackIndex = null, _BackPage = null;

    /// <summary>
    /// 数据索引值-1
    /// </summary>
    public object DataIndex
    {
        get { return _FirstId; }
        set { _FirstId = value; }
    }

    /// <summary>
    /// 其他数据索引值-2
    /// </summary>
    public object OtherIndex
    {
        get { return _OtherIndex; }
        set { _OtherIndex = value; }
    }

    /// <summary>
    /// 备份数据索引值-3
    /// </summary>
    public object BackIndex
    {
        get { return _BackIndex; }
        set { _BackIndex = value; }
    }

    /// <summary>
    /// 执行成功后，返回页面名称
    /// </summary>
    public object BackPage
    {
        get { return _BackPage; }
        set { _BackPage = value; }
    }
    
    #endregion

    public virtual void Page_Load(object sender, EventArgs e)
    {
        //链接字符非法字符的检查，防止被网络攻击
        if (Common.SafetyCheck(this.Page)) Response.End();

        //如果收到退出请求
        if (Request["exit"] != null) { Session.Abandon(); Request.Cookies.Clear(); }

        //设置页面的标题
        this.Title = IOBase.WebTitle + (string.IsNullOrEmpty(this.Title) ? "" : "-" + this.Title);

        //查找页头控件，依据登陆状态，并向其传递参数
        //Control WebHead = this.FindControl("WebHead");


        //add by andre @ 08/22/16
        HttpCookie cookieloggedIn = Request.Cookies["ioc_loggedin"];
        if (cookieloggedIn != null)
        {
            string username = cookieloggedIn.Value;
            string url_userinfo = Base_API_URI + "api/IO_Users?loginName=" + username;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url_userinfo);

                string jsonStr = null;
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream);
                        jsonStr = reader.ReadToEnd();
                    }
                }

                if (jsonStr != null)
                {
                    Dictionary<string, string> dict =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
                    if (dict["UserId"] != null)
                    {
                        int uid = Int32.Parse(dict["UserId"]);
                        WebUser user = new WebUser(uid);
                        user.GetCompanyInformation();
                        Session["User"] = user;
                    }
                }

            }
            catch (WebException)
            {}

        }
        //add by andre end


        //获取当前登陆的用户信息
        if (Session["User"] != null) { this.Overtime = false; this.CurrentUser = (WebUser)Session["User"]; } else { this.Overtime = true; Response.Redirect("../Default.aspx"); Response.End(); }

        //数据索引值
        this.DataIndex = this.Form.Attributes["Data"];
        //如果长度大于10位，说明这个数据没有解密取回
        if (this.DataIndex != null && this.DataIndex.ToString().Length > 10) this.DataIndex = Common.DecryptID(this.DataIndex); else this.DataIndex = null;

        //其他索引值
        this.OtherIndex = this.Form.Attributes["Other"];
        //如果长度大于10位，说明这个数据没有解密取回
        if (this.OtherIndex != null && this.OtherIndex.ToString().Length > 10) this.OtherIndex = Common.DecryptID(this.OtherIndex); else this.OtherIndex = null;

        //备份索引值
        this.BackIndex = this.Form.Attributes["Back"];
        //如果长度大于10位，说明这个数据没有解密取回
        if (this.BackIndex != null && this.BackIndex.ToString().Length > 10) this.BackIndex = Common.DecryptID(this.BackIndex); else this.BackIndex = null;

        OutputSettings();
    }

    /// <summary>
    /// 附加脚本或样式
    /// </summary>
    /// <param name="CssFile">样式文件，包括完整的路径</param>
    /// <param name="ScriptFile">脚本文件，包括完整的路径</param>
    public void Regist_Css_Script(string CssFile, string ScriptFile)
    {
        HtmlGenericControl AddItem;

        if (!string.IsNullOrEmpty(CssFile))
        {
            AddItem = new HtmlGenericControl("link");
            AddItem.Attributes["href"] = CssFile;
            AddItem.Attributes["rel"] = "stylesheet";
            AddItem.Attributes["type"] = "text/css";
            this.Page.Header.Controls.Add(AddItem);
        }

        if (!string.IsNullOrEmpty(ScriptFile))
        {
            AddItem = new HtmlGenericControl("script");
            AddItem.Attributes["src"] = ScriptFile;
            AddItem.Attributes["language"] = "javascript";
            AddItem.Attributes["type"] = "text/javascript";
            this.Page.Header.Controls.Add(AddItem);
        }
    }

    public void SendParm(Control Ctrl, bool LendState)
    {
        if (Ctrl == null || !LendState) return;

        System.Type My_Type;
        My_Type = Ctrl.GetType();
        System.Reflection.PropertyInfo My_Property;
        My_Property = My_Type.GetProperty("CurrentUser");

        My_Property.SetValue(Ctrl, this.CurrentUser, null);
    }

    /// <summary>
    /// 重新保存用户信息
    /// </summary>
    public void ResaveUser()
    {
        Session["User"] = this.CurrentUser;
    }

    /// <summary>
    /// 存储数据索引值
    /// </summary>
    /// <param name="index"></param>
    public void SaveIndex(object index)
    {
        this.DataIndex = index;
        this.Form.Attributes.Add("Data", this.DataIndex.ToString());
    }

    /// <summary>
    /// 存储混淆过数据索引值
    /// </summary>
    /// <param name="index"></param>
    public void SaveEncryptIndex(object index, object other)
    {
        if (index != null)
        {
            //取得正确索引值
            this.DataIndex = Common.DecryptID(index);
            //直接保存,因为接收到数据就是混淆过的
            this.Form.Attributes.Add("Data", index.ToString());
        }

        if (other != null)
        {
            //取得正确索引值
            this.OtherIndex = Common.DecryptID(other);
            //直接保存,因为接收到数据就是混淆过的
            this.Form.Attributes.Add("Other", other.ToString());
        }
    }

    /// <summary>
    /// 存储混淆过数据索引值
    /// </summary>
    /// <param name="index"></param>
    public void SaveEncryptIndex(object index, object other, object back)
    {
        if (index != null)
        {
            //取得正确索引值
            this.DataIndex = Common.DecryptID(index);
            //直接保存,因为接收到数据就是混淆过的
            this.Form.Attributes.Add("Data", index.ToString());
        }

        if (other != null)
        {
            //取得正确索引值
            this.OtherIndex = Common.DecryptID(other);
            //直接保存,因为接收到数据就是混淆过的
            this.Form.Attributes.Add("Other", other.ToString());
        }

        if (back != null)
        {
            //取得正确索引值
            this.BackIndex = Common.DecryptID(back);
            //直接保存,因为接收到数据就是混淆过的
            this.Form.Attributes.Add("Back", back.ToString());
        }
    }

    /// <summary>
    /// 显示无数据提示面板
    /// </summary>
    /// <param name="Title">面板提示标题</param>
    /// <param name="Target"></param>
    /// <param name="ControlList">需要隐藏对象</param>
    public void ShowNoPanel(string Title, string Target, Control[] ControlList)
    {
        Common.OutPutScript(this, string.Format("NoDataShow('{0}','{1}')", Title, Target));
        if (ControlList != null) foreach (Control C in ControlList) C.Visible = false;
    }

    /// <summary>
    /// 给按钮添加页面返回事件
    /// </summary>
    /// <param name="bntBack">按钮</param>
    /// <param name="Url">返回到页面</param>
    public void PageLink(Button bntBack, string Url)
    {
        bntBack.OnClientClick = string.Format("PageLink('{0}');return false;", Url);
    }

    /// <summary>
    /// 绑定数据表格
    /// </summary>
    /// <param name="SQL"></param>
    /// <param name="gdv"></param>
    /// <param name="PageIndex"></param>
    /// <returns></returns>
    public bool DataBand(string SQL, ref GridView gdv, int PageIndex)
    {
        this.IOBase.ExecuteQuery(SQL);
        gdv.PageIndex = PageIndex;
        gdv.DataSource = this.IOBase.ResultTable;
        gdv.DataBind();
        if (this.IOBase.DataRows > 0)
        {
            return true;
        }
        else
        {
            ShowNoPanel("No data to display ", "nodata", null);
        }
        return false;
    }

    /// <summary>
    /// 输出配置信息
    /// </summary>
    public void OutputSettings()
    {
        this.IOBase.ExecuteQuery(string.Format("Select * From IO_UserSetting Where UserId={0}", this.CurrentUser.UserId));

        if (this.IOBase.DataRows == 0)
        {
            //默认浮动警示信息层
            Common.OutPutScript(this, "PopAlert = false;");
        }
        else
        {
            //默认浮动警示信息层
            Common.OutPutScript(this, this.IOBase.GetRowValue("PopAlert") == "1" ? "PopAlert = true;" : "PopAlert = false;");
        }

        //输出用户信息
        Common.OutPutScript(this, string.Format("Uindex='{0}';Utype='{1}';Cindex='{2}';DateZone={3};",
                                                 Common.EncryptID(this.CurrentUser.UserId),
                                                 Common.EncryptID(this.CurrentUser.UserType),
                                                 Common.EncryptID(this.CurrentUser.CompanyId),
                                                 this.CurrentUser.UserTimeZone));
    }

    /// <summary>
    /// 单引号替换
    /// </summary>
    /// <param name="txtBox">文本输入框</param>
    /// <returns>把一个单引号替换成两个单引号  ' --->  ''</returns>
    public string SQLText(TextBox txtBox)
    {
        return txtBox.Text.Replace("'", "''").Trim();
    }

}
