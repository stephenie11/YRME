<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Photo.aspx.cs" Inherits="_Photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/comment/comment.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <form runat="server">
        <div class="container" >
            <!--Photo container-->
            <div class="image" style="background-color: #000; width: 100%;">
                <asp:Image ID="imagePage" class="img-responsive center-block" runat="server"/>
            </div>

            <!--Bottom section-->
            <div class="img-comments center-block" style="margin-top: 0px; background-color: #ffff; padding:10px 30px 30px 30px;">
                <p align="right"><asp:Label ID="DeletePhotoLabel" runat="server" Text="" ForeColor="Red"></asp:Label> Taken on <%= GetUploadDate() %>
                    <% if (HttpContext.Current.Session["ID"] != null)
                        { %>
                            <button type="button" runat="server" ID="DeletePhotoBtn" onserverclick="Delete_Photo_Button_Click" >Remove photo</button></p>
                        <%} %>
               

                <!-- Left section-->
                <div class="detailBoxLeft">
                    <!-- useravatar,username,phototitle-->
                    <div class="titleBoxLeft">
                        <div class="commenterImageLeft1">
                            <img src="<%= GetOwnerAvatar() %>" />
                        </div>
                        <div class="hasuri" style="display:inline-block; padding:5px" >
                            <h2><%= GetOwnerUsername() %></h2><br />
                            <h4><%= GetPhotoTitle() %></h4>                           
                        </div>      
                    </div>
                    <!--title album and link to it-->
                    <div class="commentBoxLeft">      
                        <h4 class="taskDescriptionLeft">This photo is in <a href="/Album/<%= GetAlbumId() %>"><%= GetAlbumTitle() %></a> album</h4>
                    </div>
                    <!--Tags-->                 
                    <div class="actionBoxLeft">
                        <a href="Default.aspx">Tags</a>
                        <!-- tags repeater-->
                        <ul class="commentListLeft">
                            <asp:Repeater ID="TagsExplorer" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="chip" style="width:30px">
                                            <a href="PhotosByTag/<%# Eval("tag_title") %>" style="text-decoration:none; color:black;"><%# Eval("tag_title") %></a>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <% if (HttpContext.Current.Session["ID"]!= null && (int)HttpContext.Current.Session["ID"] == g_owner_id)
                            { %>
                                <div class="form-groupLeft">
                                    <div class="form-group">
                                        <asp:TextBox ID="AddTagInput"  runat="server" class="form-control" placeholder="Add new tag" ></asp:TextBox>
                                        <asp:Button class="btn btn-primary" Text="Add tag" runat="server" ID="Button1" OnClick="Add_Tag_Button_Click" />
                                
                                        <asp:Label ID="AddTag_Errors" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                </div> 
                        <%} %>
                    </div>
                </div>  
                <!-- Right section-->
                <div class="detailBox">
                    <!-- nr favs end comms-->
                    
                    <div class="titleBox">
                        <% if (HttpContext.Current.Session["ID"] != null)
                            { %>
                                <button type="button" runat="server" ID="Button3" onserverclick="Like_Button_Click" ><span class="glyphicon glyphicon-thumbs-up" style="color:cornflowerblue" aria-hidden="true" ></span>Like</button>
                                <button type="button" runat="server" ID="Button4" onserverclick="Unlike_Button_Click" ><span class="glyphicon glyphicon-thumbs-down" style="color:cornflowerblue" aria-hidden="true" ></span>Unlike</button>
                        <% } else { %>
                                <span class="glyphicon glyphicon-thumbs-up" style="color:cornflowerblue" aria-hidden="true" >
                        <% } %>
                        &nbsp;<%= GetNrFavs() %>&nbsp; &nbsp; &nbsp; &nbsp; <%= GetNrComments() %> comments
                       
                    </div>
                    
                    <!-- comments-->
                    <div class="actionBox">
                        <ul class="commentList">
                            <asp:Repeater ID="explorerComments" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <% if (HttpContext.Current.Session["ID"] != null)
                                            { %>
                                                <button type ="button" runat="server" style="color:white" class="close" aria-hidden="true"  onserverclick="CloseCommBtn" >
                                                    <%# Eval("comm_id")%>&times;
                                                    <span class="glyphicon glyphicon-remove" style="color:cornflowerblue" aria-hidden="true" ></span>
                                                </button>
                                        <% } %>
                                        <div class="commenterImage">
                                          <img src="<%# Eval("commenter_avatar") %>" />
                                        </div>
                                        <div class="commentText">
                                            <p class=""><%# Eval("comment_text")%></p> <span class="date sub-text">on <%# Eval("comm_date") %></span>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                         </ul>
                     </div>

                    <!-- Add comment-->
                    <% if (HttpContext.Current.Session["ID"] != null)
                        { %>
                            <div class="addComm">
                                <div class="addComm">
                                    <asp:TextBox ID="CommInput"  runat="server" class="form-control" placeholder="Write a comment..." ></asp:TextBox>
                                    <asp:Button class="btn btn-primary" Text="Submit comment" runat="server" ID="CommBtn" OnClick="Add_Comment_Button_Click" />
                            
                                    <asp:Label ID="CommErrors" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                    <%} %>

                </div>
            </div>


        </div>
    </form>
</asp:Content>

