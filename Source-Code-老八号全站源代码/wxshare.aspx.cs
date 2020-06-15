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

public partial class wxshare : System.Web.UI.Page
{
    public string timestr = "";
    public string paySign = "";
    public string nonceStr = "Wm3WZYTPz0wzccnW";
    B_WX_APPID appBll = new B_WX_APPID();
    public M_WX_APPID appMod = new M_WX_APPID();
    M_UserAPP uappMod = new M_UserAPP();
    B_UserAPP uappBll = new B_UserAPP();
    B_User buser = new B_User();
	public M_WX_User wuserMod = new M_WX_User();    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
			int appid = DataConverter.CLng(Request.QueryString["appid"]);
			
            if (mu != null && mu.UserID > 0)
            {
                try
                {
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    timestr = Convert.ToInt64(ts.TotalSeconds).ToString();
                    if (appid == 0)
                        appid = 1;
                    WxAPI wxapi = WxAPI.Code_Get(appid);
                    appMod = appBll.SelReturnModel(appid);

                    uappMod = uappBll.SelModelByUid(mu.UserID, "wechat");
                    wuserMod = wxapi.GetWxUserModel(uappMod.OpenID);

                    userface.Src = wuserMod.HeadImgUrl;
                    userface.Alt = wuserMod.Name;
                    NickName.Text = wuserMod.Name;

                    string jsapi_ticket = APIHelper.GetWebResult("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + wxapi.AccessToken + "&type=jsapi");
                    JObject jsapi_obj = (JObject)JsonConvert.DeserializeObject(jsapi_ticket);
                    string stringA = "jsapi_ticket=" + jsapi_obj["ticket"].ToString() + "&noncestr=" + nonceStr + "&timestamp=" + timestr + "&url=" + Request.Url.AbsoluteUri;
                    paySign = EncryptHelper.SHA1(stringA).ToLower();

                    string jsapi_ticket1 = APIHelper.GetWebResult("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + wxapi.AccessToken + "&type=jsapi", "POST", "{\"action_name\":\"QR_LIMIT_STR_SCENE\",\"action_info\":{\"scene\":{\"scene_str\":\"" + mu.UserID + "\"}}}");
                    if (jsapi_ticket1 != "")
                    {
                        JObject jsapi_obj1 = (JObject)JsonConvert.DeserializeObject(jsapi_ticket1);
                        Image1.ImageUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + jsapi_obj1["ticket"];
                    }
                }
                catch (Exception ex) 
				{
					
				}
            }
        }
    }
}