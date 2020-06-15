<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wxshare.aspx.cs" Inherits="wxshare" Debug="true" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<meta charset="utf-8">
<meta name="msapplication-TileColor" content="#1A0066"/>
<meta name="msapplication-TileImage" content="/images/win8_symbol_140x140.png"/>
<title>我的推荐二维码</title>
<meta name="Keywords" content="META关键字">
<meta name="Description" content="META网页描述">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta name="renderer" content="webkit">
<link href="/dist/css/bootstrap.min.css" rel="stylesheet"/>
<!--[if lt IE 9]>
<script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
<script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>0
<![endif]-->
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;" name="viewport" />
<script type="text/javascript" src="/JS/jquery.min.js" ></script>
<script src="/dist/js/bootstrap.min.js"></script>
<link href="/dist/css/font-awesome.min.css" rel="stylesheet"/>
<link href="/dist/css/weui.min.css" rel="stylesheet"/>
<link href="<%Call.Label("{$CssDir/}");%>global.css?Version=<%Call.Label("{ZL.Label id=\"前端样式版本号\"/}");%>" rel="stylesheet"/>
<script src="https://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script>
    $.post("/BU/Client.ashx?action=isfollow").then((data) => {
        var model = APIResult.getModel(data);
        if (!APIResult.isok(model)) {
            //未关注公众账号则跳转前去关注公众账号
            location = "/Class_1/NodePage.aspx"
        }
    })
    wx.config({
        appId: '<%=appMod.APPID%>',  // 必填，公众号的唯一标识
        debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
        jsApiList: ["onMenuShareAppMessage","onMenuShareTimeline"], // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
        nonceStr: '<%=nonceStr%>', // 必填，生成签名的随机串
        signature: '<%=paySign%>',// 必填，签名，见附录1
        timestamp: '<%=timestr%>' // 必填，生成签名的时间戳
    });
    wx.ready(function () {
        // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
        wx.onMenuShareAppMessage({
            title: '我是<%=NickName.Text%>,我为氿度伞代言', // 分享标题
            desc: '氿度共享雨伞-我们致力于共享雨伞，共享经济，服务社会', // 分享描述
            link: '<%Call.Label("{$SiteURL/}");%>/wxpromo.aspx?userid=<%Call.Label("{ZL:GetuserID()/}"); %>', // 分享链接
            imgUrl: '<%=wuserMod.HeadImgUrl%>', // 分享图标
            type: '', // 分享类型,music、video或link，不填默认为link
            dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
            success: function () {
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });
        wx.onMenuShareTimeline({
            title: '我是<%=NickName.Text%>,我为氿度伞代言', // 分享标题
            desc: '氿度共享雨伞-我们致力于共享雨伞，共享经济，服务社会', // 分享描述
            link: '<%Call.Label("{$SiteURL/}");%>/wxpromo.aspx?userid=<%Call.Label("{ZL:GetuserID()/}"); %>', // 分享链接
            imgUrl: '<%=wuserMod.HeadImgUrl%>', // 分享图标
            type: '', // 分享类型,music、video或link，不填默认为link
            dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
            success: function () {
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });
    });
</script>
<style>
body { position:inherit;}
.tui_body { font-family:"STHeiti","Microsoft YaHei","黑体","arial";}/*推广页面*/
.tui_body:before { content: ' '; position: fixed; z-index: -1; top: 0; right: 0; bottom: 0; left: 0; background:url(/Template/Umbrella/style/Images/wei_cu.jpg) no-repeat; background-size:cover;}
.tui_body_t { margin-top:15px; text-align:center;}
.tui_body_tc img { box-shadow:0 0 5px #000; border-radius:4px;}
.tui_body_t h1 { margin-bottom:0.5em; font-family:"ZoomlaXingtiJ4"; color:#fff; font-size:4em;}
.tui_body_b { padding:10px; position:absolute; bottom:60px; width:100%;}
.tui_body_b p { }
.tui_body_b h3 { text-shadow:0 0 5px rgba(0,0,0,0.56); text-align:center; font-size:1.2em;}
.tui_body_b .media-left img { width:40px; height:40px;}
.tui_body_b h4 {}
.tui_body_b h4 span { color:#FFBE00;}
.tui_body_b p span { color:#FFBE00;}
.muser_bottom { background:rgba(255,255,255,0.63);}
.logos .cls-1,.logos .cls-2,.logos .cls-3{fill:#fff;}
.tui_body { font-family:"STHeiti","Microsoft YaHei","黑体","arial";}
.tui_body_tl { padding-left:2em; padding-right:2em; padding-bottom:1em;}
.tui_body_tc { text-align:center;}
.tui_body_b { padding:10px; position:absolute; bottom:60px; width:100%; background:rgba(255,255,255,0.63);}
.tui_body_b p { }
.tui_body_b h3 { margin-top:0; color:#ff7000; text-shadow:0 0 5px rgba(0,0,0,0.56); text-align:center; font-size:1.2em;}
.tui_body_b .media-left img { width:40px; height:40px;}
.tui_body_b h4 { }
.tui_body_b h4 span { color:#ff7000;}
.tui_body_b p span { color:#ff7000;}
.wei_foot { background:rgba(255,255,255,0.63);}
#mcover { position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0, 0, 0, 0.7); display:none; z-index:20000;}
#mcover img { position:fixed; right:0; top:5px; width:260px; height:180px; z-index:999;}
</style>
</head>
<body>
<form id="form1" runat="server">
<div class="tui_body">
<div class="tui_body_t">

<div class="tui_body_tl">
<img src="<%Call.Label("{$LogoUrl/}");%>" alt="<%Call.Label("{$SiteName/}");%>" style="width:50%;"/>
</div>
<div class="tui_body_tc">
<asp:Image ID="Image1" runat="server" AlternateText="推广二维码" Width="210" />
</div>
</div>
<div class="tui_body_b">
<p style="font-size:0.8em; color:#dd1937; margin-bottom:0;">
1.该二维码是您的专属转借氿度共享雨伞二维码，可直接右上角分享到朋友圈邀请好友成为氿度共享雨伞会员。<br/>
2.使用微信扫一扫功能直接转借氿度共享雨伞。<br/>
3.长按保存图片后分享到朋友圈
</p>
<div class="media">
<div class="media-left"><img class="media-object" src="" alt="" onerror="javascript:this.src='/Images/userface/noface.png';" runat="server" id="userface" /></div>
<div class="media-body">
<h4 class="media-heading">我是<asp:Label ID="NickName" runat="server" Text=""></asp:Label></h4>
<p style="margin-bottom:0;">我为<span>氿度伞</span>代言 <a href="javascript:;" class="btn btn-info" onClick="document.getElementById('mcover').style.display='block';"><i class="fa fa-share" aria-hidden="true"></i> 立即分享</a></p>
</div>
</div>
</div>
<%Call.Label("{ZL.Label id=\"微信底部\"/}"); %>
<div id="mcover" onclick="document.getElementById('mcover').style.display='';" style="display: none;"><img src="<%Call.Label("{$CssDir/}");%>images/share.png" /></div>
</div>
</form>
</body>
</html>
<script src="/JS/Controls/B_User.js"></script>
<script>
$("#foot_user").addClass("weui-bar__item_on");
</script>