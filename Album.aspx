<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs" Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
<div class="container">
    <form runat="server" style="background-color:rgba(255, 255, 255, 5); padding-bottom:5em; padding-top:2em; padding-left:2em; padding-right:2em ">
        <!--Show Album Title-->
        <h1 class="my-4 text-left text-lg-left"><%= Get_Album_Title()%></h1>

        <!--Show Album Owner-->
        <h2>by <a href="/Profile/<%= Get_UserID() %>"><%= Get_Username() %></a></h2>

        <!--Errors for upload photo-->
        <asp:Label ID="AddPhotoErrors" ForeColor="Red" runat="server"  Text=""></asp:Label>

        <div class="grid container" data-masonry='{ "itemSelector": ".grid-item", "columnWidth": 200 }'  data-infinite-scroll='{ "path": ".pagination__next", "append": ".post", "history": false }' >    
          <!-- Upload photo in album -->
            <% if (HttpContext.Current.Session["ID"]!= null && (int)HttpContext.Current.Session["ID"] == g_user_id)
                { %>
                  <div class="grid-item">
                    <asp:ImageButton ID="AddPhoto" runat="server" class="img-fluid img-thumbnail" Width="180px" ImageUrl="~/images/add-a-picture-icon-44909.png" OnClick="Upload_new_photo" />
                    <asp:FileUpload ID="upload_photo" runat="server" class="form-control" Width="180px" />                                       
                    <asp:TextBox  ID="AddPhotoTitle" runat="server" class="form-control" PlaceHolder="Add a title!" Width="180px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="add_photo_title_validation" runat="server" ControlToValidate="AddPhotoTitle" ErrorMessage="Please fill with photo title!" ForeColor="Red"></asp:RequiredFieldValidator>                                
                  </div>
            <% } %>
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
   </form>
</div>
</asp:Content>

