<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Photo.aspx.cs" Inherits="_Photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/comment/comment.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <div class="container center">
        <div class="image" style="background-color: #000; width: 100%;">
            <img src="http://placehold.it/800x500" alt="" class="img-responsive center-block" >
        </div>

        <div class="img-comments center-block" style="border: 1px solid #000; margin-top: 20px; background-color: #fff; padding: 20px;">
            <div class="row">
    <div class="col-sm-10 col-sm-offset-1">
            <center>
                    <a><span class="glyphicon glyphicon-heart"></span></a>
                    <span> 300 likes </span>
            </center>
            
            <div class="leave-comment" id="add-comment">
                    <form action="#" method="post" class="form-horizontal" id="commentForm" role="form"> 
                        <div class="form-group">
                            <label for="email" class="col-sm-2 control-label">Comment</label>
                            <div class="col-sm-10">
                              <textarea class="form-control" name="addComment" id="addComment" rows="5" style="overflow-x: hidden"></textarea>
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-10">                    
                                <button class="btn btn-success btn-circle text-uppercase" type="submit" id="submitComment"><span class="glyphicon glyphicon-send"></span> Summit comment</button>
                            </div>
                            
                        </div>  
                        
                        
                    </form>
                </div>                    
            <div class="comments">
                <div id="comments-logout">                
                    <ul class="media-list">
                      <li class="media">
                        <a class="pull-left" href="#">
                          <img class="media-object img-circle" src="https://s3.amazonaws.com/uifaces/faces/twitter/dancounsell/128.jpg" alt="profile">
                        </a>
                        <div class="media-body">
                          <div class="well well-lg">
                              <h4 class="media-heading text-uppercase reviews">Marco </h4>
                              <ul class="media-date text-uppercase reviews list-inline">
                                <li class="dd">22</li>
                                <li class="mm">09</li>
                                <li class="aaaa">2014</li>
                              </ul>
                              <p class="media-comment">
                                Great snippet! Thanks for sharing.
                              </p>
                          </div>              
                        </div>
                        
                      </li>          
                      
                     
                            </ul>  
                        </div>
                      </li>
                    </ul> 
                </div>
            </div>
	</div>
  </div>  

        </div>

    </div>
</asp:Content>

