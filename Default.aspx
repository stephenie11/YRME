<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">   
    
    <div class="categories">
        <!-- Top 5 most used tags-->


         <div class="chip">
            <a href="PhotosByTag/<%: Tag_1.Text %>" style="text-decoration:none; color:black;"><asp:Label ID="Tag_1" runat="server"></asp:Label></a>
        </div>
        <div class="chip">
            <a href="PhotosByTag/<%: Tag_2.Text %>" style="text-decoration:none; color:black;"><asp:Label ID="Tag_2" runat="server"></asp:Label></a>
        </div>
        <div class="chip">
            <a href="PhotosByTag/<%: Tag_3.Text %>" style="text-decoration:none; color:black;"><asp:Label ID="Tag_3" runat="server"></asp:Label></a>
        </div>
    </div>

    <!--Begin Dynamic Thumbnail Gallery-->
    <h1 class="my-4 text-left text-lg-left">Thumbnail Gallery</h1>
    <div class="grid container" data-masonry='{ "itemSelector": ".grid-item", "columnWidth": 200 }'  data-infinite-scroll='{ "path": ".pagination__next", "append": ".post", "history": false }' >    
        <asp:Repeater ID="explorerImages" runat="server">
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

