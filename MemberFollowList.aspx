<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberFollowList.aspx.cs" Inherits="MKForum.MemberFollowList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


        <asp:Repeater ID="rptMemberFollows" runat="server">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server"> 
                    <%# Eval("PointID").ToString() == null 
                            ? string.Format("新文章:{0}",Eval("Title")) 
                            : string.Format("{0}新回復:「{1}」","第5樓" ,Eval("PostCotent")) 
                    %> 

                </asp:Label>
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
