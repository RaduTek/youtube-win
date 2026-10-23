// Shared scripts for the app's HTML templates (IE8 compatible)

// Clicks on overlay buttons this soon after the overlay appeared are ignored. A touch or pen tap
// (emulated as a mouse) delivers hover and click at once, so the first tap only reveals the buttons.
var OVERLAY_CLICK_DELAY = 200; // ms

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
		if (show && overlay.style.display !== 'block') {
			overlay.shownAt = new Date().getTime();
			UpdateQueueButton(overlay);
		}

		overlay.style.display = show ? 'block' : 'none';
	}

	// Overlay button clicks. Handled here instead of inline onclick attributes, which would run
	// before this handler and couldn't be skipped for an ignored tap.
	container.attachEvent('onclick', function () {
		var link = event.srcElement;
		if (!link || link.tagName !== 'A') return;

		var overlay = link.parentNode;
		if (!overlay || overlay.className.indexOf('thumbnailOverlay') !== 0) return;

		if (new Date().getTime() - (overlay.shownAt || 0) < OVERLAY_CLICK_DELAY) {
			event.returnValue = false; // the javascript: href doesn't run
			return;
		}

		// update the buttons right away, the app adds / removes / downloads the video
		switch (link.className) {
			case 'thumbnailQueue':
			case 'thumbnailRemove':
				ToggleQueueButton(link);
				break;

			case 'thumbnailDownload':
				HideDownloadButton(link);
				break;
		}
	});

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

// Shows the add or remove queue button of an overlay, depending on whether its video is queued,
// and hides the download button (Browse only) when the video is downloaded or downloading.
// Only overlays with a videoid attribute (class queueToggle) have these buttons. All of them are
// rendered with the item and only their visibility is switched, so no icon has to be loaded later on.
function UpdateQueueButton(overlay) {
	var videoId = overlay.getAttribute('videoid');
	if (!videoId) return;

	var noDownload = HasDownloadButton(overlay) && !window.external.IsDownloadable(videoId);

	SetOverlayState(overlay, window.external.IsQueued(videoId), noDownload);
}

function HasDownloadButton(overlay) {
	var links = overlay.getElementsByTagName('a');
	return links[links.length - 1].className === 'thumbnailDownload';
}

function SetOverlayState(overlay, queued, noDownload) {
	overlay.className = 'thumbnailOverlay queueToggle' + (queued ? ' queued' : '') + (noDownload ? ' noDownload' : '');
}

// Click on a queue button: flip it right away, the app adds or removes the video
function ToggleQueueButton(button) {
	var overlay = button.parentNode;
	SetOverlayState(overlay, overlay.className.indexOf('queued') < 0, overlay.className.indexOf('noDownload') >= 0);
}

// Click on the download button: hide it right away, the app starts the download
function HideDownloadButton(button) {
	var overlay = button.parentNode;
	SetOverlayState(overlay, overlay.className.indexOf('queued') >= 0, true);
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
