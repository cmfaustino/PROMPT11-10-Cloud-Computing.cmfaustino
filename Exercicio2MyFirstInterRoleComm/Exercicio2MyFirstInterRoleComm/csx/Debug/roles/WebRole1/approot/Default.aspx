<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebRole1._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET! (My First Inter-Role Communication)
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
        </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>
    <p>
        <asp:Button ID="ButtonProcess" runat="server" Text="Process" OnClick="ClicarNoBotao" />
        <asp:TextBox ID="TextBoxProcess" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="LabelListaProcessMsgsSent" runat="server" 
            Text="LabelListaProcessMsgsSent:"></asp:Label>
    </p>
</asp:Content>
