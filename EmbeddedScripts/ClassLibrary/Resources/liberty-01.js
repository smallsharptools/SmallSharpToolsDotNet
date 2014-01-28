/////////////////////////////////////////////////////////////////////
// liberty - Basic JavaScript Library 0.1 by Andreas Kalsch, Trier //
// Last change: 02.08.2006 //////////////////////////////////////////
// Tutorial and API: http://aka-fotos.de/web?javascript/liberty /////
/////////////////////////////////////////////////////////////////////
// Published under General Public License: //////////////////////////
// http://www.fsf.org/licensing/licenses/gpl.html ///////////////////
/////////////////////////////////////////////////////////////////////
// More scripts: http://aka-fotos.de/web ////////////////////////////
/////////////////////////////////////////////////////////////////////

try {
	var t = undefined;
}
catch(e) {
	undefined=null;
}

// From: Prototype JavaScript framework, version 1.4.0
// (c) 2005 Sam Stephenson <sam@conio.net>
if (!Array.prototype.push)
	Array.prototype.push = function() {
		var startLength = this.length;
		for (var i = 0; i < arguments.length; i++)
			this[startLength + i] = arguments[i];
		return this.length;
	}

function $() {
	var elements = new Array();
	
	for (var i = 0; i < arguments.length; i++) {
		var element = arguments[i];
		if (typeof element == 'string')
			element = document.getElementById(element);
	
		if (arguments.length == 1)
			return element;
	
		elements.push(element);
	}
	
	return elements;
}

extendObject = function(destination, source) {
	for (var property in source)
		destination[property] = source[property];
	return destination;
}

if (!Array.prototype.indexOf)
	Array.prototype.indexOf = function(object) {
		for (var i = 0; i < this.length; i++)
			if (this[i] == object) 
				return i;
		return -1;
	}

getDimension = function(element) {
	var el = $(element);
	if (getStyle(el, 'display') != 'none')
		return {width: el.offsetWidth, height: el.offsetHeight};
	
	var els = el.style;
	var originalVisibility = els.visibility;
	var originalPosition = els.position;
	els.visibility = 'hidden';
	els.position = 'absolute';
	els.display = '';
	var obj = {width: el.clientWidth, height: el.clientHeight};
	els.display = 'none';
	els.position = originalPosition;
	els.visibility = originalVisibility;
	return obj;
}
//

uniqueId = function(prefix) {
	if (prefix == undefined)
		prefix = 'id';
		
	for (var i = 0; $(prefix+i); i++);
	return prefix+i;
}

isArray = function(object) {
	if (!window.Array || object == null)
		return false;
	return object.constructor == window.Array;
}

getLength = function(object) {
	var i = 0;
	for (var key in object)
		i++;
	return i;
}

extendObject(String.prototype, {
	escapeXML: function() {
		var value = this;
		
		value = value.replace(/&/g, '&amp;');
		value = value.replace(/</g, '&lt;');
		value = value.replace(/>/g, '&gt;');
		value = value.replace(/\"/g, '&quot;');
		value = value.replace(/\'/g, '&apos;');
		
		return value;
	},
	
	unescapeXML: function() {
		var value = this;
		
		value = value.replace(/&lt;/gi, '<');
		value = value.replace(/&gt;/gi, '>');
		value = value.replace(/&quot;/gi, '"');
		value = value.replace(/&apos;/gi, '\'');
		value = value.replace(/&amp;/gi, '&');
		
		return value;
	}
});

Number.prototype.setInRange = function(from, to) {
	if (this < from)
		return from;
	if (this > to)
		return to;
	return this;
}

getStyle = function(element, style) {
	el = $(element);
	if (typeof el == 'object' && typeof style == 'string') {
		if (el != null) {
			var value = el.style[style];
			if (value != '')
				return value;
			try {
				return el.currentStyle[style];
			} catch(e) {}
			try {
				return window.getComputedStyle(el, null)[style];
			} catch(e) {}
		}
	}
	
	return '';
}

getElements = function(tagName, attribute, value) {
	if (tagName != undefined)
		tagName = tagName.toLowerCase();
	else
		tagName = '*';
	
	if (attribute != undefined)
		attribute = attribute.toLowerCase();
	
	var children;
	if (tagName == '*') {
		children = document.getElementsByTagName('*');
		if (children.length == 0) 
			children = document.all;		
	}
	else
		children = document.getElementsByTagName(tagName);
				
	if (attribute != 'id') {
		var elements = [];
		
		for  (var i = 0; i < children.length; i++) {
			var child = children[i];
			if (attribute != undefined) {
				if (attribute == 'class') {
					values = child.className.split(' ');
					for (var j = 0; j < values.length; j++) {
						if (value instanceof RegExp && value.test(values[j]) || values[j] == value) {
							elements.push(child);
							break;
						}
					}
				}
				else if (value instanceof RegExp && value.test(child[attribute]) || child[attribute] == value || value == undefined && child[attribute] != undefined && child[attribute] != '')
					elements.push(child);
			}
			else
				elements.push(child);
		}
	
		return elements;
	}
	else
		return [document.getElementById(value)];

	return [];
}

addHandler = function(elements, types, handler) {
	if (!isArray(elements))
		elements = [elements];
	if (!isArray(types))
		types = [types];
	if (typeof handler == 'function') {
		for (var i = 0; i < elements.length; i++) {
			var el = $(elements[i]);
			
			for (var j = 0; j < types.length; j++) {
				var type = types[j].toLowerCase();
				if (type.substr(0,2) == 'on')
					type = type.substr(2);
				var ontype = 'on'+type; 
					
				for (var k = 0; typeof el[type+k] == 'function'; k++);
				if (k == 0) {
					if (el[ontype]) {
						el[type+k] = el[ontype];
						el[ontype] = null;
						k++;
					}
				}
				el[type+k] = handler;
				if (!el[ontype])
					el[ontype] = function(e) {
						if (!e)
							e = window.event;

						for (var i = 0; typeof this[e.type+i] == 'function'; i++) {
							this[e.type+i](e);
						}
					}
			}
		}
		
		return true;
	}
	
	return false;
}

removeHandler = function(elements, types, handler) {
	if (!isArray(elements))
		elements = [elements];
	if (!isArray(types))
		types = [types];
		
	var ok = false;
	for (var i = 0; i < elements.length; i++) {
		var el = $(elements[i]);
		
		for (var j = 0; j < types.length; j++) {
			var type = types[j].toLowerCase();
			if (type.substr(0,2) == 'on')
				type = type.substr(2);
			var ontype = 'on'+type; 
				
			if (typeof handler == 'function') {
				for (var k = 0; typeof el[type+k] != 'undefined'; k++) {
					if (el[type+k] == handler) {
						el[type+k] = function(e){};
						ok = true;
					}	
				}
			}
			else {
				el[ontype] = null;
				ok = true;
			}
		}
	}
	
	return ok;
}

getPosition = function(element, top, scrolling) {
	var el = $(element);
	
	if (top == undefined)
		top = document.body;
	else
		top = $(top);
	if (scrolling != undefined)
		scrolling = Math.round(Number(scrolling).setInRange(0, 1));
	else
		scrolling = 1;

	for (var lx = 0, ly = 0; el != top; el = el.offsetParent) {
		var par = el.offsetParent;
		var offsetLeft = el.offsetLeft;
		var offsetTop = el.offsetTop;
			
		if (scrolling) {
			offsetLeft -= par.scrollLeft;
			offsetTop -= par.scrollTop;
		}
		
		lx += offsetLeft;
		ly += offsetTop;
	}
	
	return {x: lx, y: ly};
}

getMousePosition = function(e, element) {
	if (!e)
		e = window.event;
	if (!e.target)
		e.target = e.srcElement;
	
	if (element == undefined)
		return {x: e.clientX, y: e.clientY};
	
	var x, y;
	if (e.layerX) {
		x = e.layerX;
		y = e.layerY;
	}
	else {
		x = e.offsetX;
		y = e.offsetY;
	}
	
	var el = $(element);
	if (el != e.target) {
		var pos = getPosition(e.target, el);
		x += pos.x;
		y += pos.y;
	}
	
	return {x: x, y: y};
}

changeDisplay = function(element) {
	el = $(element);

	var els = el.style;
	var display = getStyle(el, 'display');
	if (typeof el._display == 'undefined')
		el._display = '';
	els.display = (display == 'none') ? el._display : 'none';	
	el._display = display;	
		
	return els.display;
}
