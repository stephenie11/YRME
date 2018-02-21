<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <div class="container">
        <!-- top section--> 
        <div class="card hovercard">
            <!-- card cover-photo-->
            <div class="card-background">
                <img class="card-bkimg" alt="" src="/images/ws_Night_Lights_Blur_2560x1440.jpg"></div>
            <!--user avatar-->
            <div class="useravatar">
                <img alt="" src="<%= Get_Avatar_Path() %>"></div>
            <!--First and last name -->
            <div class="card-info">
                <span class="card-title"><%= Get_Username()%></span></div>
        </div>
        <!-- toggle button-->
        <div class="btn-pref btn-group btn-group-justified btn-group-lg" role="group" aria-label="...">
            <div class="btn-group" role="group">
                <button type="button" id="stars" class="btn btn-default" href="#tab1" data-toggle="tab"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
                    <div class="hidden-xs">About</div>
                </button>
            </div>
            <div class="btn-group" role="group">
                <button type="button" id="favorites" class="btn btn-default" href="#tab2" data-toggle="tab"><span class="glyphicon glyphicon-picture" aria-hidden="true"></span>
                    <div class="hidden-xs">Albums</div>
                </button>
            </div>
            <div class="btn-group" role="group">
                <button type="button" id="following" class="btn btn-default" href="#tab3" data-toggle="tab"><span class="glyphicon glyphicon-heart" aria-hidden="true"></span>
                    <div class="hidden-xs">Faves</div>
                </button>
            </div>
        </div>
        <!-- bottom section-->
        <div class="well">
            <div class="tab-content">
                <!--Begin About tab-->
                <div class="tab-pane fade in active" id="tab1">
                  <h3>About</h3>
                   <div class="Joined">
                       <label for="Joined"> Joined </label> 
                       <span><%= Get_Join_Date() %> </span>
                   </div>
            
                    <div class="CurrentCity">
                        <label for="CurrentCity"> Current city </label>
                        <span> <%= Get_Current_City() %> </span>
                    </div>

                    <div class="Country">
                        <label for="Country"> Country </label>
                        <span>  <%= Get_Country() %> </span>
                    </div>

                    <div class="NrPhotos">
                        <label for="NrPhotos"> Photos uploaded </label>
                        <span>  <%= Get_Photos_Number() %></span>
                    </div>
                    <div class="NrFavs">
                        <label for="NrFavs"> Favs </label>
                        <span> <%= Get_Favs_Number() %></span>
                    </div>

                    <div class="NrFaved">
                        <label for="NrFaved"> Faved </label>
                        <span> <%= Get_Faved_Number()%></span>
                    </div>
                </div><!--End About tab-->

                <!-- Albums -->
                <div class="tab-pane fade in active" id="tab2" style="height=auto">
                    <h3>Albums</h3>
                    <form runat="server">
                        <asp:Label ID="AddAlbumErrors" ForeColor="Red" runat="server"  Text=""></asp:Label>
                        <!--Album Gallery-->
                         <div class="grid container"  data-masonry='{ "itemSelector": ".grid-item", "columnWidth": 200 }'  data-infinite-scroll='{ "path": ".pagination__next", "append": ".post", "history": false }'>    
                            <!--Create new album -->
                             <% if (HttpContext.Current.Session["ID"] != null && (int)HttpContext.Current.Session["ID"] == g_user_id)
                                 { %>
                                 <div class="grid-item">
                                 
                                         <asp:ImageButton ID="AddAlbum" runat="server" class="img-fluid img-thumbnail" Width="180px" ImageUrl="~/images/newalb.PNG" OnClick="Create_new_album" />
                                         <asp:TextBox  ID="AddAlbumTitle" runat="server" class="form-control" Text="Untitled Album" Width="180px"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="add_album_title_validation" runat="server" ControlToValidate="AddAlbumTitle" ErrorMessage="Please fill with a album title!" ForeColor="Red"></asp:RequiredFieldValidator>                                
                                 
                                 </div>
                             <% } %>
                             <!--Albums Repeater-->
                             <asp:Repeater ID="explorerAlbums" runat="server">
                                <ItemTemplate>
                                    <div class="grid-item">
                                        <a href="/Album/<%# Eval("album_id") %>">
                                            <img  id="<%# Eval("photo_id") %>" class="img-fluid img-thumbnail" src="/<%# Eval("location_path") %>" />                                 
                                        </a>
                                        <p><%# Eval("album_title") %></p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </form>
                </div>

                <!--begin FAVOURITES-->
                <div class="tab-pane fade in active" id="tab3">
                    <!--Begin Dynamic Thumbnail Gallery-->
                    <h3>Faves</h3>
                    <div class="grid container"  data-masonry='{ "itemSelector": ".grid-item", "columnWidth": 200 }'  data-infinite-scroll='{ "path": ".pagination__next", "append": ".post", "history": false }'>    
                        <asp:Repeater ID="explorerFavs" runat="server">
                            <ItemTemplate>
                                <div class="grid-item">
                                    <a href="/Photo/<%# Eval("photo_id") %>">
                                        <img  id="<%# Eval("photo_id") %>" class="img-fluid img-thumbnail" src="/<%# Eval("location_path") %>" />
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div><!--End Dynamic Thumbnail Gallery-->
                </div><!-- end FAVOURITES-->            
            </div>
        </div>
    </div>  
</asp:Content>

