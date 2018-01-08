<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Edit_Profile.aspx.cs" Inherits="Edit_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <div class="container" style="background-color:white; padding-bottom:2em">
    <h1>Edit Profile</h1>
  	<hr>
	<div class="row">
         <form class="form-horizontal" role="form" enctype="multipart/form-data" runat="server" >
          <!-- left column Change Photo-->
          <div class="col-md-3">
            <div class="text-center">
                
                <img src="<%= Get_Avatar_Path() %>" class="avatar img-circle" alt="avatar" width="100px" height="100px">
                
              <h6>Upload a different photo...</h6>
                <asp:FileUpload ID="edit_profile_upload" runat="server" class="form-control"/>
              
            </div>
          </div>
      
          <!-- edit form column -->
          <div class="col-md-9 personal-info">

            <h3>Personal info</h3>

            <!--form Personal info-->        
            <!--form class="form-horizontal" role="form"-->

                <!-- First name-->
                  <div class="form-group">
                    <asp:Label class="col-lg-3 control-label" >First name:</asp:Label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="edit_first_name" runat="server" class="form-control" placeholder=""/>
                        <asp:RequiredFieldValidator ID="fs_edit_validation" runat="server" ControlToValidate="edit_first_name" ErrorMessage="Please fill with your first name!" ForeColor="Red"></asp:RequiredFieldValidator>
			    		
                    </div>
                  </div>
                <!-- Last name-->
                  <div class="form-group">
                    <asp:Label class="col-lg-3 control-label"  >Last name:</asp:Label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="edit_last_name" runat="server" class="form-control" placeholder="Doe"/>
                        <asp:RequiredFieldValidator ID="ls_edit_validation" runat="server" ControlToValidate="edit_last_name" ErrorMessage="Please fill with your last name!" ForeColor="Red"></asp:RequiredFieldValidator>
			    	</div>
                  </div>
                <!--Current City-->
                  <div class="form-group">
                    <asp:Label class="col-lg-3 control-label" >Current city:</asp:Label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="edit_current_city" runat="server" class="form-control" placeholder="Bucuresti"/>
                        <asp:RequiredFieldValidator ID="city_edit_validation" runat="server" ControlToValidate="edit_last_name" ErrorMessage="Please fill with your current city!" ForeColor="Red"></asp:RequiredFieldValidator>
			    	</div>
                  </div>
                <!--Country-->
                  <div class="form-group">
                    <asp:Label class="col-lg-3 control-label" >Country:</asp:Label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="edit_country" runat="server" class="form-control" placeholder="Romania"/>
                        <asp:RequiredFieldValidator ID="country_edit_validation" runat="server" ControlToValidate="edit_country" ErrorMessage="Please fill with your country!" ForeColor="Red"></asp:RequiredFieldValidator>
			    	</div>
                  </div>

              <div class="form-group">
                <asp:Label ID="Edit_Errors" runat="server" class="col-md-3 control-label" Text=""></asp:Label>                
                <div class="col-md-8">
                    <asp:Button class="btn btn-primary" Text="Save Changes" runat="server" ID="Edit_save" OnClick="Edit_Save_Button_Click" />
                    <span></span>
                    <asp:Button class="btn btn-primary" Text="Cancel" runat="server" ID="Edit_cancel" OnClick="Edit_Cancel_Button_Click" />                  
                </div>
              </div>
            
          </div>
        </div></form>
    </div>
<hr>
</asp:Content>

