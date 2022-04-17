function setActiveNavItem(item_id) {
    var tabs = document.querySelectorAll('.navbar-item');
    tabs.forEach(function (el) {
        var square = el.getElementsByClassName('check-square')[0]
        square.style.background = 'transparent'
    });
    activeNavItem = document.getElementById(item_id)
    var square = activeNavItem.getElementsByClassName('check-square')[0]
    square.style.background = 'white'
    var img = activeNavItem.getElementsByClassName('nav-link-img')[0]
    img.style.filter = 'invert(87%) sepia(55%) saturate(3597%) hue-rotate(231deg) brightness(103%) contrast(102%)'
    var p = activeNavItem.getElementsByClassName('nav-link-p')[0]
    p.style.color = '#23258f'

}

