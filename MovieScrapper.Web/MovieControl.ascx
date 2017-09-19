<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MovieControl.ascx.cs" Inherits="MovieScrapper.MovieControl" %>

<div class="movieItem">
   <div class="title">
        <div ="items">                                      
              <a class="linkTitle" href="<%# BuildUrl(Item.Id) %>" title="<%# Item.Overview %>"><%# Item.Title %> (<%# DisplayYear(Item.ReleaseDate) %>)</a> 
              <br />
              <a href="<%# BuildImdbUrl(Item.ImdbId) %>" target="_newtab" title="See the info in IMDB" ><img class="imdb" src="/imdb.svg" /> </a>
                                       
         </div>
     </div>
         <img class="poster" src="<%# BuildPosterUrl(Item.PosterPath) %>" />
 </div>                 

