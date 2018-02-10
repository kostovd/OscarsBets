<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NominationControl.ascx.cs" Inherits="MovieScrapper.NominationControl" %>

<div class="movieItem">
    <div class="title">
        <div class="items">
            <a class="linkTitle" href="<%# MovieDetailsUrl %>" title="<%# Item.Movie.Overview %>">
                <%# Item.Movie.Title %> (<%# MovieDisplayYear %>)
            </a>
            <br />
            <a href="<%# MovieImdbUrl %>" target="_newtab" title="See the info in IMDB">
                <img class="imdb" src="/images/icons/imdb.svg" />
            </a>
            
        </div>
        <img runat="server" visible="<%# PersonVisible %>" class="profile" src="<%# PersonProfileUrl %>" title="<%# PersonName %>" />  
    </div>
    
          
    <img id="myImg" class="poster" src="<%# MoviePosterUrl %>" />
    
    <div id="myModal" class="modal">
        <span class="close" onclick="document.getElementById('myModal').style.display='none'">&times;</span>
        <img class="modal-content">
    </div>
    
</div>


