define(["require", "exports", "d3"], function (require, exports, d3) {
    var SvgPlot = (function () {
        function SvgPlot() {
        }
        SvgPlot.prototype.validate = function (options) {
            if (options.padding.length != 4)
                throw "padding option should be an array with size 4";
        };
        SvgPlot.prototype.EstimateYTicksLabelDigits = function (y, nsteps) {
            var range = d3.extent(y);
            var digitsOnLeft = Math.max(range[0].toFixed(0).length, range[1].toFixed(0).length);
            var steps = (range[1] - range[0]) / nsteps;
            var digitsOnRight = (steps < 1.0) ? Math.round(Math.abs(Math.log(Math.abs(steps - Math.floor(steps))) / Math.log(10))) : 0;
            return digitsOnLeft + digitsOnRight + 1;
        };
        SvgPlot.prototype.plot2d = function (div, x, y, options) {
            if (x.length == 0) {
                $(div).addClass("svgplot-empty-graph");
                return;
            }
            else
                $(div).removeClass("svgplot-empty-graph");
            this.validate(options);
            var xAxisHeight = Math.ceil(Math.round(options.font_size * 2)) + options.padding[2];
            var yAxisLength = Math.ceil(this.EstimateYTicksLabelDigits(y, 5) * options.font_size * 0.8) + options.padding[3];
            var yrange = options.yrange != null ? options.yrange : d3.extent(y);
            var xs = d3.scale.linear().domain(d3.extent(x)).range([yAxisLength, options.width - options.padding[1]]);
            var ys = d3.scale.linear().domain(yrange).range([options.height - xAxisHeight, options.padding[0]]);
            var Xaxis = d3.svg.axis().scale(xs).orient("bottom");
            var Yaxis = d3.svg.axis().scale(ys).orient("left");
            d3.select(div).html("");
            var svg = d3.select(div).append("svg");
            svg.attr("width", options.width).attr("height", options.height).selectAll("circle").data(x).enter().append("circle").attr("r", options.points_size).attr("cx", function (d) {
                return xs(d);
            }).data(y).attr("cy", function (d) {
                return ys(d);
            });
            //XAxis
            svg.append("g").attr("transform", "translate(0," + (options.height - xAxisHeight) + ")").call(Xaxis).call(function (g) {
                g.selectAll("text").attr("font-size", options.font_size);
            }).call(function (g) {
                g.selectAll("path").attr("fill", "none").attr("stroke", "black").attr("shape-rendering", "crispEdges");
            });
            //YAxis
            svg.append("g").attr("transform", "translate(" + yAxisLength + ",0)").attr("class", "svg-plot-axis-group").call(Yaxis).call(function (g) {
                g.selectAll("text").attr("font-size", options.font_size);
            }).call(function (g) {
                g.selectAll("path").attr("fill", "none").attr("stroke", "black").attr("shape-rendering", "crispEdges");
            });
        };
        return SvgPlot;
    })();
    exports.SvgPlot = SvgPlot;
});
//# sourceMappingURL=svg_plots.js.map