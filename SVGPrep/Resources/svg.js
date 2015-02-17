var transMatrix = [1, 0, 0, 1, 0, 0];
var mapMatrix;
var width;
var height;
var svgDoc;
var tooltip_bg;
var svgPosition;
var svgElement;

$(document).ready(function () {

});

function handleCustProps() {
    var customProperties = document.getElementById("properties");
    svgElement = document.getElementsByTagName("svg")[0];
    //tooltip_bg = document.getElementById("tooltip_bg");
    var svg = document.getElementById("svg");
    var allcps = $("v\\:custprops");
    for (i = 0; i < allcps.length; i++) {
        var cps = allcps[i].childNodes;
        var text1 = null;
        for (j = 0; j < cps.length; j++) {
            if (cps[j].attributes != null && cps[j].attributes["v:val"] != null && cps[j].attributes["v:val"].childNodes[0].data != "VT4(N/A)" && cps[j].attributes["v:lbl"].nodeValue != "" && cps[j].attributes["v:val"].nodeValue != "VT4()") {
                if (text1 == null) {
                    var group = allcps[i].parentNode;
                    text1 = document.createElement("div");
                    text1.setAttribute("id", "txt" + group.id + "1");
                    text1.style.display = "none";
                    text1.style.maxHeight = "600px";
                    text1.style.height = "600px";
                    text1.style.overflowY = "hidden";
                    group.onmousemove = function (evt) { ShowTooltip(evt) };
                    group.onmouseout = function (evt) { HideTooltip(evt) };
                }
                var tspan1 = document.createElement("span");
                tspan1.style.fontWeight = "bold";
                var data1 = document.createTextNode(cps[j].attributes["v:lbl"].nodeValue + ": ");
                tspan1.appendChild(data1);

                var tspan2 = document.createElement("tspan");
                var data2 = document.createTextNode(cps[j].attributes["v:val"].nodeValue.substring(4, cps[j].attributes["v:val"].nodeValue.length - 1));
                tspan2.appendChild(data2);

                text1.appendChild(tspan1);
                text1.appendChild(tspan2);
                text1.appendChild(document.createElement("br"));
            }
        }
        if (text1 != null) {
            customProperties.appendChild(text1);
        }
    }
}

function createLayers() {
    var layerKeys = new Array();
    var layerData = new Object();
    var nav = document.getElementById("nav");
    var layers = $("v\\:layer");
    for (i = 0; i < layers.length; i++) {
        var chkid = layers[i].getAttribute("v:name");
        var chkindex = layers[i].getAttribute("v:index");

        if (chkid) {
            layerKeys.push(chkid);
            layerData[chkid] = chkindex;
        }
    }

    var sortedKeys = layerKeys.sort();

    for (i = 0; i < sortedKeys.length; i++) {
        var cid = sortedKeys[i];
        var cindex = layerData[cid];

        var chk = document.createElement("input");
        chk.type = "checkbox";
        chk.id = cid;
        chk.value = cindex;
        chk.checked = "checked";
        chk.onclick = function () {
            toggleLayer(this.value, this.id);
        }

        var literal = document.createElement("span")
        literal.innerHTML = cid + "<br/>";

        nav.appendChild(chk);
        nav.appendChild(literal);
    }
}

function toggleLayer(layer, c) {
    var chk = document.getElementById(c);
    var gs = $("g");

    for (i = 0; i < gs.length; i++) {
        var element = gs[i]
        var atts = element.attributes;
        if (atts["v:layermember"] && atts["v:layermember"].nodeValue == layer) {
            if (chk.checked) {
                element.style.visibility = "visible";
            }
            else {
                element.style.visibility = "hidden";
            }
        }
    }
}

function init(evt) {
    if (window.svgDocument == null) {
        svgDoc = evt.target.ownerDocument;
    }
    mapMatrix = svgDoc.getElementById("all");
    width = evt.target.getAttributeNS(null, "width");
    height = evt.target.getAttributeNS(null, "height");
    svgDoc = evt.target.ownerDocument;
}

function pan(dx, dy) {
    transMatrix[4] += dx;
    transMatrix[5] += dy;

    newMatrix = "matrix(" + transMatrix.join(' ') + ")";
    mapMatrix.setAttributeNS(null, "transform", newMatrix);
}

function zoom(scale) {
    for (var i = 0; i < transMatrix.length; i++) {
        transMatrix[i] *= scale;
    }
    transMatrix[4] += (1 - scale) * width / 2;
    transMatrix[5] += (1 - scale) * height / 2;
    newMatrix = "matrix(" + transMatrix.join(' ') + ")";
    mapMatrix.setAttributeNS(null, "transform", newMatrix);
}

function ShowTooltip(evt) {
    var tooltip1 = document.getElementById("txt" + evt.currentTarget.id + "1");
    tooltip1.style.display = "inline";
}

function HideTooltip(evt) {
    var tooltip1 = document.getElementById("txt" + evt.currentTarget.id + "1");
    tooltip1.style.display = "none";
}