
function showLinks(id) {
    var linksDiv = document.getElementById(id);
    var scriptTags = document.getElementsByTagName('script');
    var i, j = 1;
    var newContent = '';
    for (i=0;i<scriptTags.length;i++) {
        if (scriptTags[i].src != '' && scriptTags[i].src.indexOf("WebResource.axd") != -1) {
            newContent = newContent + '<a href="' + scriptTags[i].src +
            '">script ' + (j++) + '</a><br />\n';
        }
    }
    linksDiv.innerHTML = newContent;
}
