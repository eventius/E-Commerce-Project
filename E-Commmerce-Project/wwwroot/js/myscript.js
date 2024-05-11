// Toggle Navbar Menu
var MenuItems = document.getElementById('menuItems');

MenuItems.style.maxHeight = "0px";

function menuToggle() {
  if (MenuItems.style.maxHeight == "0px") {

      MenuItems.style.maxHeight = "400px";
      MenuItems.style.padding = "30px";

  }else{
      MenuItems.style.maxHeight = "0px";
      MenuItems.style.padding = "0px";
  }
}

// $('#btnmin').click(function(){
//   var number= parseFloat($('#number').val());
//   number=number-1;
//   if(number<=0)
//   {number=0};
//   $('#number').val(number);
//   total=number*34;
//   $("#tprice").html(total);});
  
// $('#btnplus').click(function(){
//   var number= parseFloat($('#number').val());
//   number=number+1;
//   $('#number').val(number);
//   total=number*34;
//   $("#tprice").html(total);
// });

$('.hcarousel').hcarousel({
  interval: 2000
});
$('.carousel').carousel({
  interval: 500
});
// Get the modal
var modal = document.getElementById("myModal");

// Get the button that opens the modal
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal 
btn.onclick = function() {
  modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function() {
  modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
}