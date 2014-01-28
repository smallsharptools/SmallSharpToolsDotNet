(function() {

    if (window["console"] === undefined) {
        window["console"] = function() { };
        console.log = function() { };
    }

    var benchmark = function(name, fn) {
        console.log('benchmarking');
        return function() {
            var start = new Date();
            var returnValue = fn.apply(this, [], []);
            var end = new Date();
            var duration = end.getTime() - start.getTime();
            console.log({ name: name, duration: duration });
            return returnValue;
        };
    };

    window.benchmark = benchmark;

    // usage: fn = benchmark('a name', fn);

    var baseNamespace = 'SST';

    var $createNamespace = function(str) {
        var parts = str.split('.');
        var ns = undefined;
        for (var i = 0; i < parts.length; i++) {
            if (i === 0) {
                if (window[parts[i]] === undefined) {
                    ns = window[parts[i]] = {};
                }
                else {
                    ns = window[parts[i]];
                }
            }
            else {
                if (ns !== undefined) {
                    if (ns[parts[i]] === undefined) {
                        ns = ns[parts[i]] = {};
                    }
                    else {
                        ns = ns[parts[i]];
                    }
                }
            }
        }
    };

    $createNamespace(baseNamespace);

    // usage: SST.CreateNamespace('Acme.Services.Users');

    $sb = function(text) {
        this.buffer = [];
        if (text !== null) {
            this.append(text);
        }
    };

    $sb.prototype.append = function(text) {
        this.buffer[this.buffer.length] = text;
        return this;
    };

    $sb.prototype.clear = function() {
        this.buffer = [];
    };

    $sb.prototype.toString = function() {
        return this.buffer.join('');
    };

    window[baseNamespace].StringBuilder = $sb;
    window[baseNamespace].CreateNamespace = $createNamespace;

    // usage: var sb = new SST.StringBuilder();
    // sb.append('John').append(' ').append('Smith');
    // var name = sb.toString();

} ());
