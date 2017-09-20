$(document).ready(function () {
	// Get the modal
	
	var modal = document.getElementById('myModal');
	var movies = $('.movieItem');
	movies.each(function () {
		var img = $('#myImg');
		img.onclick = function () {
			modal.style.display = "block";
			modalImg.src = this.src;
			captionText.innerHTML = this.alt;
		}
		var modalImg = $('#img01');
		var captionText = $('#caption');
	});
	// Get the image and insert it inside the modal - use its "alt" text as a caption
	
	//var modalImg = document.getElementById("img01");
	//var captionText = document.getElementById("caption");
	

	// Get the <span> element that closes the modal
	var span = document.getElementsByClassName("close")[0];

	// When the user clicks on <span> (x), close the modal
	span.onclick = function () {
		modal.style.display = "none";
	}
	
})