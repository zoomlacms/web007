<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Example.aspx.cs" Inherits="ZoomLaCMS.Tools.Example" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<asp:GridView runat="server" ID="EGV" AutoGenerateColumns="true"></asp:GridView>
<script src="/dist/vue/vue.js"></script>
<div id="banner_wrap">
    <div v-for="item in list">{{item.name}}</div>
</div>
<script>var js = document.createElement("script"); js.src="/Tools/1.js?" + Math.random();document.body.appendChild(js)</script>
<script>
    //new Vue({
    //    el: "#banner_wrap",
    //    data: {
    //        list: [{ name: "test" }, {name:"v2"}],//多张图
    //        model:{},//第一张图
    //    }
    //});
</script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Script">

</asp:Content>