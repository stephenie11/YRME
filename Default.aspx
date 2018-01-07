<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">   
    
    <div class="categories">
        <div class="chip">
              Landscapes <span class="closebtn" onclick="this.parentElement.style.display='none'">&times;</span>
        </div>

        <div class="chip">
              Cars <span class="closebtn" onclick="this.parentElement.style.display='none'">&times;</span>
        </div>

        <div class="chip">
              Boats <span class="closebtn" onclick="this.parentElement.style.display='none'">&times;</span>
        </div>
    </div>

    <h1 class="my-4 text-left text-lg-left">Thumbnail Gallery</h1>
    <div class="grid container" data-masonry='{ "itemSelector": ".grid-item", "columnWidth": 200 }'  data-infinite-scroll='{ "path": ".pagination__next", "append": ".post", "history": false }' >
        <div class="grid-item">
            <asp:HyperLink NavigateUrl="Photo.aspx" runat="server" class="d-block mb-4 h-100">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
          </asp:HyperLink>
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="C:\Users\StefaniaPirvu\Desktop\YrmeApp\images\animals_hero_penguin_02_1.jpg" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="https://www.slovenia.info/uploads/narava/Mountains-and-hills-julian-alps.jpg" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://www.telegraph.co.uk/content/dam/Travel/galleries/travel/activityandadventure/The-worlds-most-beautiful-mountains/mountains-fitzroy_3374108a.jpg" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="C:\Users\StefaniaPirvu\Desktop\YrmeApp\images\animals_hero_penguin_02_1.jpg" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <div class="grid-item">
            <img class="img-fluid img-thumbnail" src="http://placehold.it/400x300" alt="" />
        </div>
         <!-- test infinite scroll -->

        

    </div>
</asp:Content>

