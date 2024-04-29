
function setupCarouselAdjustment(dotNetRef) {
    function adjustItems() {
        const viewportWidth = window.innerWidth;
        dotNetRef.invokeMethodAsync('AdjustItemsPerSlide', viewportWidth);
    }

    window.addEventListener('resize', adjustItems);
    adjustItems(); 

    window.cleanupCarouselAdjustment = () => {
        window.removeEventListener('resize', adjustItems);
    };
}



function scrollToBottom(elementId) {
    var element = document.getElementById(elementId);
    if (element) {
        element.scrollTop = element.scrollHeight;
    } else {
        console.error("Element not found:", elementId);
    }
}

