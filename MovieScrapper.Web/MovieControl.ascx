<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MovieControl.ascx.cs" Inherits="MovieScrapper.MovieControl" %>
<%--<script>
    // Get the modal
    var modal = document.getElementById('myModal');
    var movies = jQuery('.movieItem')
    $('.movieItem').each(function () {
        $(this).data("test", test);//refers to jquery object.
    });

    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = document.getElementById('myImg');
    var modalImg = document.getElementById("img01");
    var captionText = document.getElementById("caption");
    img.onclick = function () {
        modal.style.display = "block";
        modalImg.src = this.src;
        captionText.innerHTML = this.alt;
    }

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

</script>--%>

<div class="movieItem">
   <div class="title">
        <div ="items">                                      
              <a class="linkTitle" href="<%# BuildUrl(Item.Id) %>" title="<%# Item.Overview %>"><%# Item.Title %> (<%# DisplayYear(Item.ReleaseDate) %>)</a> 
              <br />
              <a href="<%# BuildImdbUrl(Item.ImdbId) %>" target="_newtab" title="See the info in IMDB" ><img class="imdb" src="/imdb.svg" /> </a>
                                       
         </div>
     </div >   
     <img id= "myImg" class="poster" src="<%# BuildPosterUrl(Item.PosterPath) %>" />
         
    <div id="myModal" class="modal">
         <span class="close" onclick="document.getElementById('myModal').style.display='none'">&times;</span>
         <img class="modal-content" id="img01" >
         <div id="caption"></div>
     </div>
</div>
                

