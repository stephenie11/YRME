<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Photos_By_Tag.aspx.cs" Inherits="_Photos_By_Tag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    
    <div class="chip">
        <a href="/PhotosByTag/<%: Page.RouteData.Values["tag"] %>" style="text-decoration:none; color:black;">
            <asp:Label ID="Tag" runat="server"><%: Page.RouteData.Values["tag"] %></asp:Label></a>
    </div>
    <!--Begin Dynamic Thumbnail Gallery-->
    <h1 class="my-4 text-left text-lg-left">Everyone's photos </h1>
    <div class="grid container" data-masonry='{ "itemSelector": ".grid-item", "columnWidth": 200 }'  data-infinite-scroll='{ "path": ".pagination__next", "append": ".post", "history": false }' >    
        <asp:Repeater ID="explorerImagesByTag" runat="server">
            <ItemTemplate>
                <div class="grid-item">
                    <a href="/Photo/<%# Eval("photo_id") %>">
                        <img id="<%# Eval("photo_id") %>" class="img-fluid img-thumbnail" src="/<%# Eval("location_path") %>"/>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div><!--End Dynamic Thumbnail Gallery-->
</asp:Content>

