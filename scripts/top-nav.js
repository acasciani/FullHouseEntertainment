$(document).ready(function(){
	$('.navbar.top').affix({
	  offset: {
	    top: function () {
	      return (this.top = $('.body').offset().top);
	    }
	  }
	});
});