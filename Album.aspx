<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">

    <h1 class="my-4 text-left text-lg-left"><%= Get_Album_Title()%></h1>
    <h2>by <a href="/Profile/<%= Get_UserID() %>"><%= Get_Username() %></a></h2>
    <div class="grid container" data-masonry='{ "itemSelector": ".grid-item", "columnWidth": 200 }'  data-infinite-scroll='{ "path": ".pagination__next", "append": ".post", "history": false }' >    
        <asp:Repeater ID="explorerAlbumPage" runat="server">
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

