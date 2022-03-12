<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLogin.ascx.cs" Inherits="MKForum.ucLogin" %>


<style> 
        .black_overlay{ 
            display: none; 
            position: absolute; 
            top: 0%; 
            left: 0%; 
            width: 100%; 
            height: 100%; 
            background-color: black; 
            z-index:1001; 
            -moz-opacity: 0.8; 
            opacity:.80; 
            filter: alpha(opacity=88); 
        } 
        .white_content { 
            display: none; 
            position: absolute; 
            top: 25%; 
            left: 25%; 
            width: 55%; 
            height: 55%; 
            padding: 20px; 
            border: 10px solid orange; 
            background-color: white; 
            z-index:1002; 
            overflow: auto; 
        } 
    </style> 

<a href = "javascript:void(0)" onclick = "document.getElementById('login').style.display='block';document.getElementById('fade').style.display='block'">登入</a> 


        <div id="login" class="white_content">
            <div>
      <asp:PlaceHolder ID="plcReg" runat="server">

            <p>帳號:<asp:TextBox ID="txtAcc" runat="server" placeholder="請輸入帳號" width="200px"></asp:TextBox>
          
           
            <p>密碼:<asp:TextBox ID="txtPwd" runat="server"  placeholder="數字、大小寫字母，最少8個字符、最長12個字符" width="200px"></asp:TextBox></p>
          
            <asp:Button ID="btnlogin" runat="server" Text="提交" OnClick="btnlogin_Click" />
          <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </asp:PlaceHolder>


       <asp:PlaceHolder ID="plcInfo" runat="server">
            <p>成功登入</p>
            <asp:Literal ID="ltlAccount" runat="server" Text="成功login"></asp:Literal><br />
            
      </asp:PlaceHolder>






        </div>

<a href = "javascript:void(0)" onclick = "document.getElementById('login').style.display='none';document.getElementById('fade').style.display='none'">點這裡關閉本視窗</a>

           


        </div> 
        <div id="fade" class="black_overlay">

                  



        </div> 