var isLoop = true;

if (document.querySelectorAll(".swiper-slide").length < 3) {
  isLoop = false;
}

const swiper = new Swiper(".swiper", {
  loop: isLoop,
  slidesPerView: 4,
  slidesPerGroup: 1,
  centerInsufficientSlides: true,
  freeMode: true,
  grabCursor: true,
  autoplay: {
    delay: 0,
    disableOnInteraction: false,
    pauseOnMouseEnter: true,
  },
  speed: 10000,
});
