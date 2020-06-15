using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.Common;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model;
using Newtonsoft.Json;
using System.Web.Security;
using ZoomLa.Components;
using ZoomLa.Model.Plat;
using ZoomLa.BLL.Plat;
using Newtonsoft.Json.Linq;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Model.User;
using ZoomLa.BLL.User.Addon;

public partial class wxbind : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_UserAPP uappBll = new B_UserAPP();
    M_UserAPP uappMod = new M_UserAPP();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            M_UserInfo mu = buser.GetLogin();
            int appid = DataConverter.CLng(Request.QueryString["appid"]);
            string err = "";
            if(!B_User.CheckUserStatus(mu,ref err))
            {
                function.WriteErrMsg(err);
            }
            uappMod = uappBll.SelModelByUid(mu.UserID, "wechat");
            if (uappMod != null)
            {
                function.WriteErrMsg("您已经绑定了微信公众号，不能重复绑定！");
            }
            else
            {
                if (appid > 0) { bind_img.Src = "/Common/Common.ashx?url=" + HttpUtility.UrlEncode("http://www.zhichongw.com/wxpromo.aspx?bindid=" + mu.UserID + "&appid=" + appid); }
                else { bind_img.Src = "/Common/Common.ashx?url=" + SiteConfig.SiteInfo.SiteUrl + "/wxpromo.aspx?bindid=" + mu.UserID; }
            }
        }
    }
}