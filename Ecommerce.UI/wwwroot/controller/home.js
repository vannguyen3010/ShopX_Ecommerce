$(document).ready(function () {

    // Khởi tạo slide cho bên trái
    $('#left-carousel').owlCarousel({
        nav: false,
        dots: true,
        loop: true,
        items: 1,
        margin: 10,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });

    // Khởi tạo slide cho bên phải
    $('#right-carousel').owlCarousel({
        nav: false,
        dots: true,
        loop: true,
        items: 1,
        margin: 10,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });
});
function setActiveImage(element, imagePath) {
    // Xóa lớp 'active' khỏi tất cả các thumbnail
    const thumbs = document.querySelectorAll('.product-thumb');
    thumbs.forEach(thumb => thumb.classList.remove('active'));

    // Thêm lớp 'active' cho thumbnail được nhấp
    element.classList.add('active');

    // Cập nhật hình ảnh lớn
    const mainImage = document.querySelector('.product-single-carousel img');
    mainImage.src = imagePath;
    mainImage.dataset.zoomImage = imagePath.replace('600x675', '800x900'); // Cập nhật hình ảnh zoom
}