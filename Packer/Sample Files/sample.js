(function(){

  if (window["console"] === undefined)
  {
    window["console"] = function() {};
    console.log = function() {};
  }

  var benchmark = function(name, fn) {
    console.log('benchmarking');
    return function() {
      var start = new Date();
      var returnValue = fn.apply(this, [], []);
      var end = new Date();
      var duration = end.getTime() - start.getTime();
      console.log({ name : name, duration : duration });
      return returnValue;
    };
  };
  
  window.benchmark = benchmark;
  
  // usage: fn = benchmark('a name', fn);
  
  var baseNamespace = 'SST';
  
  $sb = function(text) {
      this.buffer = [];
      if (text !== null)
      {
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

  if (window[baseNamespace] === undefined) {
      window[baseNamespace] = {};
  }

  window[baseNamespace].StringBuilder = $sb;
  
  // usage: var sb = new SST.StringBuilder();
  // sb.append('John').append(' ').append('Smith');
  // var name = sb.toString();

}());
