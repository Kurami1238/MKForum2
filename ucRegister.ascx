<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRegister.ascx.cs" Inherits="MKForum.ucRegister" %>

 
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

 <script>
     window.onload = function () {
         var validateCode = document.getElementById("validateCode");
         validateCode.onclick = function () {
             document.getElementById("imgCode").src = "Models/CaptHandler1.ashx?id=" + new Date().getMilliseconds();
         }

         function openDialog() {
             document.getElementById('reg').style.display = 'block';
             document.getElementById('fade').style.display = 'block';
         }
         function closeDialog() {
             document.getElementById('reg').style.display = 'none';
             document.getElementById('fade').style.display = 'none';
         }

 </script>


<a href = "javascript:void(0)" onclick = "openDialog()">註冊</a> 


        <div id="reg" class="white_content">
            <div>
      <asp:PlaceHolder ID="plcReg" runat="server">

            <p>帳號:<asp:TextBox ID="txtAcc" runat="server" placeholder="有數字、限定小寫字母 3-8位數" width="200px"></asp:TextBox>
            <asp:Button ID="btnAcc" runat="server" Text="帳號驗證" OnClick="btnAcc_Click" />
            <asp:Literal ID="ltlAcc" runat="server" Text="-"></asp:Literal></p>
           
            <p>密碼:<asp:TextBox ID="txtPwd" runat="server"  placeholder="數字、大小寫字母，最少8個字符、最長12個字符" width="200px"></asp:TextBox></p>
            
            <p>E-mail:<asp:TextBox ID="txtMail" runat="server" placeholder="XX@XX.com"></asp:TextBox><br /></p>
            驗證碼: <asp:TextBox ID="txtCapt" runat="server" placeholder="請輸入驗證碼"></asp:TextBox>
            
            <img src="Models/CaptHandler.ashx" /><a href="javascript:void(0)" id="validateCode"></a>
            <asp:Button ID="btnSubmit" runat="server" Text="Sumbit" OnClick="btnSubmit_Click" /><br />
            <asp:Literal ID="ltlReg" runat="server" Text="-"></asp:Literal>

            <%--<asp:HiddenField runat="server"ID="hfID" Value='<%# Eval("MemberID") %>' />--%>
        </asp:PlaceHolder>


       <asp:PlaceHolder ID="plcInfo" runat="server">
            <p>恭喜註冊成功</p>
            <asp:Literal ID="ltlAccount" runat="server" Text="成功Reg"></asp:Literal><br />
            請按登入，進入網站!
      </asp:PlaceHolder>






        </div>

<a href = "javascript:void(0)" onclick = "document.getElementById('reg').style.display='none';
    document.getElementById('fade').style.display='none'">點這裡關閉本視窗</a>

           


        </div> 
        <div id="fade" class="black_overlay">


        </div> 


              