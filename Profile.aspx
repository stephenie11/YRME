<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <div class="container">
    <div class="card hovercard">
        <div class="card-background">
            <img class="card-bkimg" alt="" src="http://lorempixel.com/100/100/people/9/">
            <!-- http://lorempixel.com/850/280/people/9/ -->
        </div>
        <div class="useravatar">
            <img alt="" src="http://lorempixel.com/100/100/people/9/">
        </div>
        <div class="card-info"> <span class="card-title">Pamela Anderson</span>

        </div>
    </div>
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

        <div class="well">
      <div class="tab-content">
        <div class="tab-pane fade in active" id="tab1">
          <h3>About</h3>
           <div class="Joined">
               <label for="Joined"> Joined </label> 
               <span><%: DateTime.Now %> </span>
               
               
           </div>
            
            <div class="CurrentCity">
                <label for="CurrentCity"> Current city </label>
                <span> Bucuresti </span>
            </div>

            <div class="Country">
                <label for="Country"> Country </label>
                <span> Romania </span>
            </div>

        </div>
        <div class="tab-pane fade in" id="tab2">
          <h3>Albums</h3>
        </div>
        <div class="tab-pane fade in" id="tab3">
          <h3>Favourites</h3>
        </div>
      </div>
    </div>
    
    </div>
            
    
</asp:Content>

