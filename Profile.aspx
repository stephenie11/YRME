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
                <button type="button" id="stars" class="btn btn-primary" href="#tab1" data-toggle="tab"><span class="glyphicon glyphicon-info-sign" aria-hidden="true"></span>
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
                <div class="tab-pane fade in" id="tab2">
                     <div class="grid container"  data-masonry='{ "itemSelector": ".grid-item", "columnWidth": 200 }'  data-infinite-scroll='{ "path": ".pagination__next", "append": ".post", "history": false }'>    
                        <asp:Repeater ID="explorerAlbums" runat="server">
                            <ItemTemplate>
                                <div class="grid-item">
                                    <a href="/Album/<%# Eval("album_id") %>">
                                        <img  id="<%# Eval("photo_id") %>" class="img-fluid img-thumbnail" src="/<%# Eval("location_path") %>" />
                                    </a>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <!--begin FAVOURITES-->
                <div class="tab-pane fade in" id="tab3">
                    <!--Begin Dynamic Thumbnail Gallery-->

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

