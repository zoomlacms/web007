<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wxbind.aspx.cs" Inherits="wxbind" Debug="true" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<meta charset="utf-8">
<meta name="msapplication-TileColor" content="#1A0066"/>
<meta name="msapplication-TileImage" content="/images/win8_symbol_140x140.png"/>
<title>绑定微信公众号</title>
<%Call.Label("{$MetaKeywords/}"); %>
<%Call.Label("{$MetaDescription/}"); %>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta name="renderer" content="webkit">
<link href="/dist/css/bootstrap.min.css" rel="stylesheet"/>
<!--[if lt IE 9]>
<script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
<script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
<![endif]-->
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;" name="viewport" />
<script type="text/javascript" src="/JS/jquery-1.11.1.min.js" ></script>
<script src="/dist/js/bootstrap.min.js"></script>
<link href="/dist/css/font-awesome.min.css" rel="stylesheet"/>
<link href="<%Call.Label("{$CssDir/}");%>global.css?Version=<%Call.Label("{ZL.Label id=\"前端样式版本号\"/}");%>" rel="stylesheet"/>
<style>
body { position:inherit;}
.muser_bottom { background:rgba(255,255,255,0.63);}
.logos .cls-1,.logos .cls-2,.logos .cls-3{fill:#fff;}
.tui_body { font-family:"STHeiti","Microsoft YaHei","黑体","arial";}
.tui_body_tl { padding-left:2em; padding-right:2em; padding-top:1em; padding-bottom:2em;}
.tui_body_tc { text-align:center;}
.tui_body_b { padding:15px; position:absolute; bottom:60px; width:100%; background:rgba(255,255,255,0.63);}
.tui_body_b p { }
.tui_body_b h3 { margin-top:0; color:#ff7000; text-shadow:0 0 5px rgba(0,0,0,0.56); text-align:center; font-size:1.2em;}
.tui_body_b .media-left img { width:40px; height:40px;}
.tui_body_b h4 { }
.tui_body_b h4 span { color:#ff7000;}
.tui_body_b p span { color:#ff7000;}
.wei_foot { background:rgba(255,255,255,0.63);}
</style>
</head>
<body class="wxbind_body">
<form id="form1" runat="server">
<div class="wxbind_body_c">
<h1>绑定微信公众号</h1>
<img src="/Common/Common.ashx?url=/wxpromo.aspx?bindid=16" alt="二维码" width="280" height="280" runat="server" id="bind_img" />
<p>请使用微信扫描二维码<br />或识别图中二维码绑定用户<br />办酒网</p>
</div>
</form>
</body>
</html>