// Shared scripts for the app's HTML templates (IE8 compatible)

// Shows the hover overlay (.thumbnailOverlay) of video thumbnails inside the container.
// Handled on the container, so items added or re-rendered later work too.
function AttachThumbnailHover(container) {
	if (!container) return;

	function findThumbnail(el) {
		while (el && el !== container) {
			if (el.className === 'thumbnail') return el;
			el = el.parentNode;
		}
		return null;
	}

	function setOverlay(thumb, show) {
		var overlay = thumb.getElementsByTagName('div')[0]; // .thumbnailOverlay
		if (!overlay) return;

		// mouseover also fires when moving between the overlay's children, only update when it appears
		if (show && overlay.style.display !== 'block') UpdateQueueButton(overlay);

		overlay.style.display = show ? 'block' : 'none';
	}

	container.attachEvent('onmouseover', function () {
		var thumb = findThumbnail(event.srcElement);
		if (thumb) setOverlay(thumb, true);
	});

	container.attachEvent('onmouseout', function () {
		var thumb = findThumbnail(event.srcElement);
		// ignore moves between children of the same thumbnail (overlay, buttons)
		if (thumb && !(event.toElement && thumb.contains(event.toElement))) setOverlay(thumb, false);
	});
}

// Shows the add or remove queue button of an overlay, depending on whether its video is queued.
// Only overlays with a videoid attribute (class queueToggle) have both buttons. Both are rendered
// with the item and only their visibility is switched, so no icon has to be loaded later on.
function UpdateQueueButton(overlay) {
	var videoId = overlay.getAttribute('videoid');
	if (!videoId) return;

	SetQueued(overlay, window.external.IsQueued(videoId));
}

function SetQueued(overlay, queued) {
	overlay.className = 'thumbnailOverlay queueToggle' + (queued ? ' queued' : '');
}

// Click on a queue button: flip it right away, the app adds or removes the video
function ToggleQueueButton(button) {
	var overlay = button.parentNode;
	SetQueued(overlay, overlay.className.indexOf('queued') < 0);
}

// Selects a tab: the n-th link in the tabs element and the n-th page in the content element
function SelectTabIn(tabsId, contentId, index) {
	var tabs = document.getElementById(tabsId);
	var content = document.getElementById(contentId);

	for (var i = 0; i < tabs.children.length; i++) {
		tabs.children[i].className = (i == index) ? "selected" : "";
		content.children[i].className = (i == index) ? "" : "hidden";
	}
}
