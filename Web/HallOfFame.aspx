<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="HallOfFame.aspx.cs" Inherits="HallOfFame" Title="Hall of Fame" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<div id="gloryList">
                    <asp:Label CssClass="headline" ID="lblWinners" Text="Liste der Gewinner" runat="server"></asp:Label>
                    <div class="lh4"><br /></div>
                    <table id="lstOfWinners" cellspacing="10px" class="centerControl">
                        <tr>
                            <th colspan=2 >Name</th>
                            <th>Jahr</th>
                        </tr>
                        <tr>
                            <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Katharina</td>
                            <td>98/99</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Annegret</td>
                            <td>99/00</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Carsten + Friedemann</td>
                            <td>00/01</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Carsten</td>
                            <td>01/02</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Carsten + Christoph</td>
                            <td>02/03</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Katharina</td>
                            <td>03/04</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Torsten</td>
                            <td>04/05</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Carsten + Peggy</td>
                            <td>05/06</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Torsten</td>
                            <td>06/07</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Torsten</td>
                            <td>07/08</td>
                        </tr>
                        <tr>
                        <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Daniel H.</td>
                            <td>08/09</td>
                        </tr>
                        <tr>
                            <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Daniel H.</td>
                            <td>09/10</td>
                        </tr>
                        <tr>
                            <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Christoph + Torsten</td>
                            <td>10/11</td>
                        </tr>
                        <tr>
                            <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Katharina</td>
                            <td>11/12</td>
                        </tr>
                        <tr>
                            <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Christoph + Carsten</td>
                            <td>12/13</td>
                        </tr>
                        <tr>
                            <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Christoph</td>
                            <td>13/14</td>
                        </tr>
                        <tr>
                            <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Manja</td>
                            <td>14/15</td>
                        </tr>
                        <tr>
                            <td><img src="images/weltmeisterpokal.gif" alt=""  /></td>
                            <td>Carsten</td>
                            <td>15/16</td>
                        </tr>
                    </table>
                </div>
</asp:Content>

