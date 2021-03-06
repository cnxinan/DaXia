/*!Extend matchMedia.js*/
(function(a) {
	a.matchMedia = (function() {
		var e = 0,
			c = "gmu-media-detect",
			b = a.fx.transitionEnd,
			f = a.fx.cssPrefix,
			d = a("<style></style>").append("." + c + "{" + f + "transition: width 0.001ms; width: 0; position: absolute; top: -10000px;}\n").appendTo("head");
		return function(i) {
			var k = c + e++,
				j, h = [],
				g;
			d.append("@media " + i + " { #" + k + " { width: 1px; } }\n");
			if ("matchMedia" in window) {
				return window.matchMedia(i)
			}
			j = a('<div class="' + c + '" id="' + k + '"></div>').appendTo("body").on(b, function() {
				g.matches = j.width() === 1;
				a.each(h, function(l, m) {
					a.isFunction(m) && m.call(g, g)
				})
			});
			g = {
				matches: j.width() === 1,
				media: i,
				addListener: function(l) {
					h.push(l);
					return this
				},
				removeListener: function(m) {
					var l = h.indexOf(m);~
					l && h.splice(l, 1);
					return this
				}
			};
			return g
		}
	}())
})(Zepto);
/*!Extend throttle.js*/
(function(a) {
	a.extend(a, {
		throttle: function(b, c, g) {
			var e = 0,
				d;
			if (typeof c !== "function") {
				g = c;
				c = b;
				b = 250
			}

			function f() {
				var k = this,
					l = Date.now() - e,
					j = arguments;

				function i() {
					e = Date.now();
					c.apply(k, j)
				}

				function h() {
					d = undefined
				}
				if (g && !d) {
					i()
				}
				d && clearTimeout(d);
				if (g === undefined && l > b) {
					i()
				} else {
					d = setTimeout(g ? h : i, g === undefined ? b - l : b)
				}
			}
			f._zid = c._zid = c._zid || a.proxy(c)._zid;
			return f
		},
		debounce: function(b, d, c) {
			return d === undefined ? a.throttle(250, b, false) : a.throttle(b, d, c === undefined ? false : c !== false)
		}
	})
})(Zepto);
/*!Extend event.scrollStop.js*/
(function(c, d) {
	function b() {
		c(d).on("scroll", c.debounce(80, function() {
			c(d).trigger("scrollStop")
		}, false))
	}

	function a() {
		c(d).off("scroll");
		b()
	}
	b();
	c(d).on("pageshow", function(f) {
		f.persisted && c(d).off("touchstart", a).one("touchstart", a)
	})
})(Zepto, window);
/*!Extend fix.js*/
(function(a, b) {
	a.extend(a.fn, {
		fix: function(e) {
			var d = this;
			if (d.attr("isFixed")) {
				return d
			}
			d.css(e).css("position", "fixed").attr("isFixed", true);
			var h = a('<div style="position:fixed;top:10px;"></div>').appendTo("body"),
				f = h[0].getBoundingClientRect().top,
				c = function() {
					if (window.pageYOffset > 0) {
						if (h[0].getBoundingClientRect().top !== f) {
							d.css("position", "absolute");
							g();
							a(window).on("scrollStop", g);
							a(window).on("ortchange", g)
						}
						a(window).off("scrollStop", c);
						h.remove()
					}
				}, g = function() {
					d.css({
						top: window.pageYOffset + (e.bottom !== b ? window.innerHeight - d.height() - e.bottom : (e.top || 0)),
						left: e.right !== b ? document.body.offsetWidth - d.width() - e.right : (e.left || 0)
					});
					e.width == "100%" && d.css("width", document.body.offsetWidth)
				};
			a(window).on("scrollStop", c);
			return d
		}
	})
}(Zepto));
/*!Extend highlight.js*/
(function(c) {
	var e = c(document),
		a, d;

	function b() {
		var f = a.attr("hl-cls");
		clearTimeout(d);
		a.removeClass(f).removeAttr("hl-cls");
		a = null;
		e.off("touchend touchmove touchcancel", b)
	}
	c.fn.highlight = function(g, f) {
		return this.each(function() {
			var h = c(this);
			h.css("-webkit-tap-highlight-color", "rgba(255,255,255,0)").off("touchstart.hl");
			g && h.on("touchstart.hl", function(j) {
				var i;
				a = f ? (i = c(j.target).closest(f, this)) && i.length && i : h;
				if (a) {
					a.attr("hl-cls", g);
					d = setTimeout(function() {
						a.addClass(g)
					}, 100);
					e.on("touchend touchmove touchcancel", b)
				}
			})
		})
	}
})(Zepto);
/*!Extend imglazyload.js*/
(function(b) {
	var a = [];
	b.fn.imglazyload = function(c) {
		var g = Array.prototype.splice,
			c = b.extend({
				threshold: 0,
				container: window,
				urlName: "data-url",
				placeHolder: "",
				eventName: "scrollStop",
				innerScroll: false,
				isVertical: true
			}, c),
			l = b(c.container),
			m = c.isVertical,
			f = b.isWindow(l.get(0)),
			h = {
				win: [m ? "scrollY" : "scrollX", m ? "innerHeight" : "innerWidth"],
				img: [m ? "top" : "left", m ? "height" : "width"]
			}, d = b(c.placeHolder).length ? b(c.placeHolder) : null,
			i = b(this).is("img");
		!f && (h.win = h.img);

		function n(r) {
			var q = f ? window : l.offset(),
				o = q[h.win[0]],
				p = q[h.win[1]];
			return o >= r[h.img[0]] - c.threshold - p && o <= r[h.img[0]] + r[h.img[1]]
		}
		a = Array.prototype.slice.call(b(a.reverse()).add(this), 0).reverse();
		if (b.isFunction(b.fn.imglazyload.detect)) {
			e();
			return this
		}

		function k(r) {
			var p = b(r),
				o = {}, q = p;
			if (!i) {
				b.each(p.get(0).attributes, function() {~
					this.name.indexOf("data-") && (o[this.name] = this.value)
				});
				q = b("<img />").attr(o)
			}
			p.trigger("startload");
			q.on("load", function() {
				!i && p.replaceWith(q);
				p.trigger("loadcomplete");
				q.off("load")
			}).on("error", function() {
				var s = b.Event("error");
				p.trigger(s);
				s.defaultPrevented || a.push(r);
				q.off("error").remove()
			}).attr("src", p.attr(c.urlName))
		}

		function j() {
			var o, p, q, r;
			for (o = a.length; o--;) {
				p = b(r = a[o]);
				q = p.offset();
				n(q) && (g.call(a, o, 1), k(r))
			}
		}

		function e() {
			!i && d && b(a).append(d)
		}
		b(document).ready(function() {
			e();
			j()
		});
		!c.innerScroll && b(window).on(c.eventName + " ortchange", function() {
			j()
		});
		b.fn.imglazyload.detect = j;
		return this
	}
})(Zepto);
/*!Extend iscroll.js*/
/*
 * iScroll v4.2.2 ~ Copyright (c) 2012 Matteo Spinelli, http://cubiq.org
 * Released under MIT license, http://cubiq.org/license
 */
(function(h, E) {
	var u = Math,
		o = [],
		l = E.createElement("div").style,
		z = (function() {
			var H = "webkitT,MozT,msT,OT,t".split(","),
				G, F = 0,
				m = H.length;
			for (; F < m; F++) {
				G = H[F] + "ransform";
				if (G in l) {
					return H[F].substr(0, H[F].length - 1)
				}
			}
			return false
		})(),
		D = z ? "-" + z.toLowerCase() + "-" : "",
		k = s("transform"),
		x = s("transitionProperty"),
		j = s("transitionDuration"),
		n = s("transformOrigin"),
		B = s("transitionTimingFunction"),
		e = s("transitionDelay"),
		A = (/android/gi).test(navigator.appVersion),
		r = (/hp-tablet/gi).test(navigator.appVersion),
		i = s("perspective") in l,
		y = "ontouchstart" in h && !r,
		d = !! z,
		f = s("transition") in l,
		g = "onorientationchange" in h ? "orientationchange" : "resize",
		b = y ? "touchstart" : "mousedown",
		t = y ? "touchmove" : "mousemove",
		c = y ? "touchend" : "mouseup",
		w = y ? "touchcancel" : "mouseup",
		a = (function() {
			if (z === false) {
				return false
			}
			var m = {
				"": "transitionend",
				webkit: "webkitTransitionEnd",
				Moz: "transitionend",
				O: "otransitionend",
				ms: "MSTransitionEnd"
			};
			return m[z]
		})(),
		q = (function() {
			return h.requestAnimationFrame || h.webkitRequestAnimationFrame || h.mozRequestAnimationFrame || h.oRequestAnimationFrame || h.msRequestAnimationFrame || function(m) {
				return setTimeout(m, 1)
			}
		})(),
		p = (function() {
			return h.cancelRequestAnimationFrame || h.webkitCancelAnimationFrame || h.webkitCancelRequestAnimationFrame || h.mozCancelRequestAnimationFrame || h.oCancelRequestAnimationFrame || h.msCancelRequestAnimationFrame || clearTimeout
		})(),
		C = i ? " translateZ(0)" : "",
		v = function(G, m) {
			var H = this,
				F;
			H.wrapper = typeof G == "object" ? G : E.getElementById(G);
			H.wrapper.style.overflow = "hidden";
			H.scroller = H.wrapper.children[0];
			H.translateZ = C;
			H.options = {
				hScroll: true,
				vScroll: true,
				x: 0,
				y: 0,
				bounce: true,
				bounceLock: false,
				momentum: true,
				lockDirection: true,
				useTransform: true,
				useTransition: false,
				topOffset: 0,
				checkDOMChanges: false,
				handleClick: true,
				onRefresh: null,
				onBeforeScrollStart: function(I) {
					I.preventDefault()
				},
				onScrollStart: null,
				onBeforeScrollMove: null,
				onScrollMove: null,
				onBeforeScrollEnd: null,
				onScrollEnd: null,
				onTouchEnd: null,
				onDestroy: null
			};
			for (F in m) {
				H.options[F] = m[F]
			}
			H.x = H.options.x;
			H.y = H.options.y;
			H.options.useTransform = d && H.options.useTransform;
			H.options.useTransition = f && H.options.useTransition;
			H.scroller.style[x] = H.options.useTransform ? D + "transform" : "top left";
			H.scroller.style[j] = "0";
			H.scroller.style[n] = "0 0";
			if (H.options.useTransition) {
				H.scroller.style[B] = "cubic-bezier(0.33,0.66,0.66,1)"
			}
			if (H.options.useTransform) {
				H.scroller.style[k] = "translate(" + H.x + "px," + H.y + "px)" + C
			} else {
				H.scroller.style.cssText += ";position:absolute;top:" + H.y + "px;left:" + H.x + "px"
			}
			H.refresh();
			H._bind(g, h);
			H._bind(b);
			if (H.options.checkDOMChanges) {
				H.checkDOMTime = setInterval(function() {
					H._checkDOMChanges()
				}, 500)
			}
		};
	v.prototype = {
		enabled: true,
		x: 0,
		y: 0,
		steps: [],
		scale: 1,
		currPageX: 0,
		currPageY: 0,
		pagesX: [],
		pagesY: [],
		aniTime: null,
		isStopScrollAction: false,
		handleEvent: function(F) {
			var m = this;
			switch (F.type) {
				case b:
					if (!y && F.button !== 0) {
						return
					}
					m._start(F);
					break;
				case t:
					m._move(F);
					break;
				case c:
				case w:
					m._end(F);
					break;
				case g:
					m._resize();
					break;
				case a:
					m._transitionEnd(F);
					break
			}
		},
		_checkDOMChanges: function() {
			if (this.moved || this.animating || (this.scrollerW == this.scroller.offsetWidth * this.scale && this.scrollerH == this.scroller.offsetHeight * this.scale)) {
				return
			}
			this.refresh()
		},
		_resize: function() {
			var m = this;
			setTimeout(function() {
				m.refresh()
			}, A ? 200 : 0)
		},
		_pos: function(m, F) {
			m = this.hScroll ? m : 0;
			F = this.vScroll ? F : 0;
			if (this.options.useTransform) {
				this.scroller.style[k] = "translate(" + m + "px," + F + "px) scale(" + this.scale + ")" + C
			} else {
				m = u.round(m);
				F = u.round(F);
				this.scroller.style.left = m + "px";
				this.scroller.style.top = F + "px"
			}
			this.x = m;
			this.y = F
		},
		_start: function(K) {
			var J = this,
				F = y ? K.touches[0] : K,
				G, m, L, I, H;
			if (!J.enabled) {
				return
			}
			if (J.options.onBeforeScrollStart) {
				J.options.onBeforeScrollStart.call(J, K)
			}
			if (J.options.useTransition) {
				J._transitionTime(0)
			}
			J.moved = false;
			J.animating = false;
			J.distX = 0;
			J.distY = 0;
			J.absDistX = 0;
			J.absDistY = 0;
			J.dirX = 0;
			J.dirY = 0;
			J.isStopScrollAction = false;
			if (J.options.momentum) {
				if (J.options.useTransform) {
					G = getComputedStyle(J.scroller, null)[k].replace(/[^0-9\-.,]/g, "").split(",");
					m = +G[4];
					L = +G[5]
				} else {
					m = +getComputedStyle(J.scroller, null).left.replace(/[^0-9-]/g, "");
					L = +getComputedStyle(J.scroller, null).top.replace(/[^0-9-]/g, "")
				} if (u.round(m) != u.round(J.x) || u.round(L) != u.round(J.y)) {
					J.isStopScrollAction = true;
					if (J.options.useTransition) {
						J._unbind(a)
					} else {
						p(J.aniTime)
					}
					J.steps = [];
					J._pos(m, L);
					if (J.options.onScrollEnd) {
						J.options.onScrollEnd.call(J)
					}
				}
			}
			J.startX = J.x;
			J.startY = J.y;
			J.pointX = F.pageX;
			J.pointY = F.pageY;
			J.startTime = K.timeStamp || Date.now();
			if (J.options.onScrollStart) {
				J.options.onScrollStart.call(J, K)
			}
			J._bind(t, h);
			J._bind(c, h);
			J._bind(w, h)
		},
		_move: function(K) {
			var H = this,
				F = y ? K.touches[0] : K,
				G = F.pageX - H.pointX,
				m = F.pageY - H.pointY,
				L = H.x + G,
				J = H.y + m,
				I = K.timeStamp || Date.now();
			if (H.options.onBeforeScrollMove) {
				H.options.onBeforeScrollMove.call(H, K)
			}
			H.pointX = F.pageX;
			H.pointY = F.pageY;
			if (L > 0 || L < H.maxScrollX) {
				L = H.options.bounce ? H.x + (G / 2) : L >= 0 || H.maxScrollX >= 0 ? 0 : H.maxScrollX
			}
			if (J > H.minScrollY || J < H.maxScrollY) {
				J = H.options.bounce ? H.y + (m / 2) : J >= H.minScrollY || H.maxScrollY >= 0 ? H.minScrollY : H.maxScrollY
			}
			H.distX += G;
			H.distY += m;
			H.absDistX = u.abs(H.distX);
			H.absDistY = u.abs(H.distY);
			if (H.absDistX < 6 && H.absDistY < 6) {
				return
			}
			if (H.options.lockDirection) {
				if (H.absDistX > H.absDistY + 5) {
					J = H.y;
					m = 0
				} else {
					if (H.absDistY > H.absDistX + 5) {
						L = H.x;
						G = 0
					}
				}
			}
			H.moved = true;
			H._beforePos ? H._beforePos(J, m) && H._pos(L, J) : H._pos(L, J);
			H.dirX = G > 0 ? -1 : G < 0 ? 1 : 0;
			H.dirY = m > 0 ? -1 : m < 0 ? 1 : 0;
			if (I - H.startTime > 300) {
				H.startTime = I;
				H.startX = H.x;
				H.startY = H.y
			}
			if (H.options.onScrollMove) {
				H.options.onScrollMove.call(H, K)
			}
		},
		_end: function(K) {
			if (y && K.touches.length !== 0) {
				return
			}
			var I = this,
				O = y ? K.changedTouches[0] : K,
				L, N, G = {
					dist: 0,
					time: 0
				}, m = {
					dist: 0,
					time: 0
				}, H = (K.timeStamp || Date.now()) - I.startTime,
				M = I.x,
				J = I.y,
				F;
			I._unbind(t, h);
			I._unbind(c, h);
			I._unbind(w, h);
			if (I.options.onBeforeScrollEnd) {
				I.options.onBeforeScrollEnd.call(I, K)
			}
			if (!I.moved) {
				if (y && this.options.handleClick && !I.isStopScrollAction) {
					I.doubleTapTimer = setTimeout(function() {
						I.doubleTapTimer = null;
						L = O.target;
						while (L.nodeType != 1) {
							L = L.parentNode
						}
						if (L.tagName != "SELECT" && L.tagName != "INPUT" && L.tagName != "TEXTAREA") {
							N = E.createEvent("MouseEvents");
							N.initMouseEvent("click", true, true, K.view, 1, O.screenX, O.screenY, O.clientX, O.clientY, K.ctrlKey, K.altKey, K.shiftKey, K.metaKey, 0, null);
							N._fake = true;
							L.dispatchEvent(N)
						}
					}, 0)
				}
				I._resetPos(400);
				if (I.options.onTouchEnd) {
					I.options.onTouchEnd.call(I, K)
				}
				return
			}
			if (H < 300 && I.options.momentum) {
				G = M ? I._momentum(M - I.startX, H, -I.x, I.scrollerW - I.wrapperW + I.x, I.options.bounce ? I.wrapperW : 0) : G;
				m = J ? I._momentum(J - I.startY, H, -I.y, (I.maxScrollY < 0 ? I.scrollerH - I.wrapperH + I.y - I.minScrollY : 0), I.options.bounce ? I.wrapperH : 0) : m;
				M = I.x + G.dist;
				J = I.y + m.dist;
				if ((I.x > 0 && M > 0) || (I.x < I.maxScrollX && M < I.maxScrollX)) {
					G = {
						dist: 0,
						time: 0
					}
				}
				if ((I.y > I.minScrollY && J > I.minScrollY) || (I.y < I.maxScrollY && J < I.maxScrollY)) {
					m = {
						dist: 0,
						time: 0
					}
				}
			}
			if (G.dist || m.dist) {
				F = u.max(u.max(G.time, m.time), 10);
				I.scrollTo(u.round(M), u.round(J), F);
				if (I.options.onTouchEnd) {
					I.options.onTouchEnd.call(I, K)
				}
				return
			}
			I._resetPos(200);
			if (I.options.onTouchEnd) {
				I.options.onTouchEnd.call(I, K)
			}
		},
		_resetPos: function(G) {
			var m = this,
				H = m.x >= 0 ? 0 : m.x < m.maxScrollX ? m.maxScrollX : m.x,
				F = m.y >= m.minScrollY || m.maxScrollY > 0 ? m.minScrollY : m.y < m.maxScrollY ? m.maxScrollY : m.y;
			if (H == m.x && F == m.y) {
				if (m.moved) {
					m.moved = false;
					if (m.options.onScrollEnd) {
						m.options.onScrollEnd.call(m)
					}
					if (m._afterPos) {
						m._afterPos()
					}
				}
				return
			}
			m.scrollTo(H, F, G || 0)
		},
		_transitionEnd: function(F) {
			var m = this;
			if (F.target != m.scroller) {
				return
			}
			m._unbind(a);
			m._startAni()
		},
		_startAni: function() {
			var K = this,
				F = K.x,
				m = K.y,
				I = Date.now(),
				J, H, G;
			if (K.animating) {
				return
			}
			if (!K.steps.length) {
				K._resetPos(400);
				return
			}
			J = K.steps.shift();
			if (J.x == F && J.y == m) {
				J.time = 0
			}
			K.animating = true;
			K.moved = true;
			if (K.options.useTransition) {
				K._transitionTime(J.time);
				K._pos(J.x, J.y);
				K.animating = false;
				if (J.time) {
					K._bind(a)
				} else {
					K._resetPos(0)
				}
				return
			}
			G = function() {
				var L = Date.now(),
					N, M;
				if (L >= I + J.time) {
					K._pos(J.x, J.y);
					K.animating = false;
					if (K.options.onAnimationEnd) {
						K.options.onAnimationEnd.call(K)
					}
					K._startAni();
					return
				}
				L = (L - I) / J.time - 1;
				H = u.sqrt(1 - L * L);
				N = (J.x - F) * H + F;
				M = (J.y - m) * H + m;
				K._pos(N, M);
				if (K.animating) {
					K.aniTime = q(G)
				}
			};
			G()
		},
		_transitionTime: function(m) {
			m += "ms";
			this.scroller.style[j] = m
		},
		_momentum: function(L, F, J, m, N) {
			var K = 0.0006,
				G = u.abs(L) * (this.options.speedScale || 1) / F,
				H = (G * G) / (2 * K),
				M = 0,
				I = 0;
			if (L > 0 && H > J) {
				I = N / (6 / (H / G * K));
				J = J + I;
				G = G * J / H;
				H = J
			} else {
				if (L < 0 && H > m) {
					I = N / (6 / (H / G * K));
					m = m + I;
					G = G * m / H;
					H = m
				}
			}
			H = H * (L < 0 ? -1 : 1);
			M = G / K;
			return {
				dist: H,
				time: u.round(M)
			}
		},
		_offset: function(m) {
			var G = -m.offsetLeft,
				F = -m.offsetTop;
			while (m = m.offsetParent) {
				G -= m.offsetLeft;
				F -= m.offsetTop
			}
			if (m != this.wrapper) {
				G *= this.scale;
				F *= this.scale
			}
			return {
				left: G,
				top: F
			}
		},
		_bind: function(G, F, m) {
			o.concat([F || this.scroller, G, this]);
			(F || this.scroller).addEventListener(G, this, !! m)
		},
		_unbind: function(G, F, m) {
			(F || this.scroller).removeEventListener(G, this, !! m)
		},
		destroy: function() {
			var G = this;
			G.scroller.style[k] = "";
			G._unbind(g, h);
			G._unbind(b);
			G._unbind(t, h);
			G._unbind(c, h);
			G._unbind(w, h);
			if (G.options.useTransition) {
				G._unbind(a)
			}
			if (G.options.checkDOMChanges) {
				clearInterval(G.checkDOMTime)
			}
			if (G.options.onDestroy) {
				G.options.onDestroy.call(G)
			}
			for (var F = 0, m = o.length; F < m;) {
				o[F].removeEventListener(o[F + 1], o[F + 2]);
				o[F] = null;
				F = F + 3
			}
			o = []
		},
		refresh: function() {
			var m = this,
				F;
			m.wrapperW = m.wrapper.clientWidth || 1;
			m.wrapperH = m.wrapper.clientHeight || 1;
			m.minScrollY = -m.options.topOffset || 0;
			m.scrollerW = u.round(m.scroller.offsetWidth * m.scale);
			m.scrollerH = u.round((m.scroller.offsetHeight + m.minScrollY) * m.scale);
			m.maxScrollX = m.wrapperW - m.scrollerW;
			m.maxScrollY = m.wrapperH - m.scrollerH + m.minScrollY;
			m.dirX = 0;
			m.dirY = 0;
			if (m.options.onRefresh) {
				m.options.onRefresh.call(m)
			}
			m.hScroll = m.options.hScroll && m.maxScrollX < 0;
			m.vScroll = m.options.vScroll && (!m.options.bounceLock && !m.hScroll || m.scrollerH > m.wrapperH);
			F = m._offset(m.wrapper);
			m.wrapperOffsetLeft = -F.left;
			m.wrapperOffsetTop = -F.top;
			m.scroller.style[j] = "0";
			m._resetPos(400)
		},
		scrollTo: function(m, L, K, J) {
			var I = this,
				H = m,
				G, F;
			I.stop();
			if (!H.length) {
				H = [{
					x: m,
					y: L,
					time: K,
					relative: J
				}]
			}
			for (G = 0, F = H.length; G < F; G++) {
				if (H[G].relative) {
					H[G].x = I.x - H[G].x;
					H[G].y = I.y - H[G].y
				}
				I.steps.push({
					x: H[G].x,
					y: H[G].y,
					time: H[G].time || 0
				})
			}
			I._startAni()
		},
		scrollToElement: function(m, G) {
			var F = this,
				H;
			m = m.nodeType ? m : F.scroller.querySelector(m);
			if (!m) {
				return
			}
			H = F._offset(m);
			H.left += F.wrapperOffsetLeft;
			H.top += F.wrapperOffsetTop;
			H.left = H.left > 0 ? 0 : H.left < F.maxScrollX ? F.maxScrollX : H.left;
			H.top = H.top > F.minScrollY ? F.minScrollY : H.top < F.maxScrollY ? F.maxScrollY : H.top;
			G = G === undefined ? u.max(u.abs(H.left) * 2, u.abs(H.top) * 2) : G;
			F.scrollTo(H.left, H.top, G)
		},
		scrollToPage: function(G, F, I) {
			var H = this,
				m, J;
			I = I === undefined ? 400 : I;
			if (H.options.onScrollStart) {
				H.options.onScrollStart.call(H)
			}
			m = -H.wrapperW * G;
			J = -H.wrapperH * F;
			if (m < H.maxScrollX) {
				m = H.maxScrollX
			}
			if (J < H.maxScrollY) {
				J = H.maxScrollY
			}
			H.scrollTo(m, J, I)
		},
		disable: function() {
			this.stop();
			this._resetPos(0);
			this.enabled = false;
			this._unbind(t, h);
			this._unbind(c, h);
			this._unbind(w, h)
		},
		enable: function() {
			this.enabled = true
		},
		stop: function() {
			if (this.options.useTransition) {
				this._unbind(a)
			} else {
				p(this.aniTime)
			}
			this.steps = [];
			this.moved = false;
			this.animating = false
		},
		isReady: function() {
			return !this.moved && !this.animating
		}
	};

	function s(m) {
		if (z === "") {
			return m
		}
		m = m.charAt(0).toUpperCase() + m.substr(1);
		return z + m
	}
	l = null;
	if (typeof exports !== "undefined") {
		exports.iScroll = v
	} else {
		h.iScroll = v
	}(function(H, F, K) {
		if (!H) {
			return
		}
		var G = F.iScroll,
			J = [].slice,
			m = (function() {
				var L = {}, N = 0,
					M = "_sid";
				return function(P, Q) {
					var O = P[M] || (P[M] = ++N);
					Q !== K && (L[O] = Q);
					Q === null && delete L[O];
					return L[O]
				}
			})(),
			I;
		F.iScroll = I = function(O, N) {
			var M = [].slice.call(arguments, 0),
				L = new G(O, N);
			m(O, L);
			return L
		};
		I.prototype = G.prototype;
		H.fn.iScroll = function(N) {
			var M = J.call(arguments, 1),
				P = typeof N === "string" && N,
				L, O;
			H.each(this, function(Q, R) {
				O = m(R) || I(R, H.isPlainObject(N) ? N : K);
				if (P === "this") {
					L = O;
					return false
				} else {
					if (P) {
						if (!H.isFunction(O[P])) {
							throw new Error("iScroll\u6ca1\u6709\u6b64\u65b9\u6cd5\uff1a" + P)
						}
						L = O[P].apply(O, M);
						if (L !== K && L !== O) {
							return false
						}
						L = K
					}
				}
			});
			return L !== K ? L : this
		}
	})(h.Zepto || null, h)
})(window, document);
/*!Extend offset.js*/
(function(c) {
	var b = c.fn.offset,
		a = Math.round;

	function d(e) {
		return this.each(function(g) {
			var h = c(this),
				i = c.isFunction(e) ? e.call(this, g, h.offset()) : e,
				f = h.css("position"),
				j = f === "absolute" || f === "fixed" || h.position();
			if (f === "relative") {
				j.top -= parseFloat(h.css("top")) || parseFloat(h.css("bottom")) * -1 || 0;
				j.left -= parseFloat(h.css("left")) || parseFloat(h.css("right")) * -1 || 0
			}
			parentOffset = h.offsetParent().offset(), props = {
				top: a(i.top - (j.top || 0) - parentOffset.top),
				left: a(i.left - (j.left || 0) - parentOffset.left)
			};
			if (f == "static") {
				props.position = "relative"
			}
			if (i.using) {
				i.using.call(this, props, g)
			} else {
				h.css(props)
			}
		})
	}
	c.fn.offset = function(e) {
		return e ? d.call(this, e) : b.call(this)
	}
})(Zepto);
/*!Extend parseTpl.js*/
(function(a, b) {
	a.parseTpl = function(f, e) {
		var c = "var __p=[];with(obj||{}){__p.push('" + f.replace(/\\/g, "\\\\").replace(/'/g, "\\'").replace(/<%=([\s\S]+?)%>/g, function(g, h) {
			return "'," + h.replace(/\\'/, "'") + ",'"
		}).replace(/<%([\s\S]+?)%>/g, function(g, h) {
			return "');" + h.replace(/\\'/, "'").replace(/[\r\n\t]/g, " ") + "__p.push('"
		}).replace(/\r/g, "\\r").replace(/\n/g, "\\n").replace(/\t/g, "\\t") + '\');}return __p.join("");',
			d = new Function("obj", c);
		return e ? d(e) : d
	}
})(Zepto);
/*!Extend touch.js*/
(function(i) {
	var g = {}, b, k, h, e = 750,
		a;

	function c(m) {
		return "tagName" in m ? m : m.parentNode
	}

	function j(n, m, p, o) {
		var r = Math.abs(n - m),
			q = Math.abs(p - o);
		return r >= q ? (n - m > 0 ? "Left" : "Right") : (p - o > 0 ? "Up" : "Down")
	}

	function l() {
		a = null;
		if (g.last) {
			g.el.trigger("longTap");
			g = {}
		}
	}

	function d() {
		if (a) {
			clearTimeout(a)
		}
		a = null
	}

	function f() {
		if (b) {
			clearTimeout(b)
		}
		if (k) {
			clearTimeout(k)
		}
		if (h) {
			clearTimeout(h)
		}
		if (a) {
			clearTimeout(a)
		}
		b = k = h = a = null;
		g = {}
	}
	i(document).ready(function() {
		var m, n;
		i(document.body).bind("touchstart", function(o) {
			m = Date.now();
			n = m - (g.last || m);
			g.el = i(c(o.touches[0].target));
			b && clearTimeout(b);
			g.x1 = o.touches[0].pageX;
			g.y1 = o.touches[0].pageY;
			if (n > 0 && n <= 250) {
				g.isDoubleTap = true
			}
			g.last = m;
			a = setTimeout(l, e)
		}).bind("touchmove", function(o) {
			d();
			g.x2 = o.touches[0].pageX;
			g.y2 = o.touches[0].pageY;
			if (Math.abs(g.x1 - g.x2) > 10) {
				o.preventDefault()
			}
		}).bind("touchend", function(o) {
			d();
			if ((g.x2 && Math.abs(g.x1 - g.x2) > 30) || (g.y2 && Math.abs(g.y1 - g.y2) > 30)) {
				h = setTimeout(function() {
					g.el.trigger("swipe");
					g.el.trigger("swipe" + (j(g.x1, g.x2, g.y1, g.y2)));
					g = {}
				}, 0)
			} else {
				if ("last" in g) {
					k = setTimeout(function() {
						var p = i.Event("tap");
						p.cancelTouch = f;
						g.el.trigger(p);
						if (g.isDoubleTap) {
							g.el.trigger("doubleTap");
							g = {}
						} else {
							b = setTimeout(function() {
								b = null;
								g.el.trigger("singleTap");
								g = {}
							}, 250)
						}
					}, 0)
				}
			}
		}).bind("touchcancel", f);
		i(window).bind("scroll", f)
	});
	["swipe", "swipeLeft", "swipeRight", "swipeUp", "swipeDown", "doubleTap", "tap", "singleTap", "longTap"].forEach(function(n) {
		i.fn[n] = function(m) {
			return this.bind(n, m)
		}
	})
})(Zepto);
/*!Extend gmu.js*/
var gmu = gmu || {
	version: "@version",
	$: window.Zepto,
	staticCall: (function(c) {
		var b = c.fn,
			d = [].slice,
			a = c();
		a.length = 1;
		return function(f, e) {
			a[0] = f;
			return b[e].apply(a, d.call(arguments, 2))
		}
	})(Zepto)
};
/*!Extend event.js*/
(function(e, f) {
	var i = [].slice,
		g = /\s+/,
		a = function() {
			return false
		}, d = function() {
			return true
		};

	function k(l, n, m) {
		(l || "").split(g).forEach(function(o) {
			m(o, n)
		})
	}

	function h(l) {
		return new RegExp("(?:^| )" + l.replace(" ", " .* ?") + "(?: |$)")
	}

	function c(l) {
		var m = ("" + l).split(".");
		return {
			e: m[0],
			ns: m.slice(1).sort().join(" ")
		}
	}

	function b(l, m, q, n) {
		var p, o;
		o = c(m);
		o.ns && (p = h(o.ns));
		return l.filter(function(r) {
			return r && (!o.e || r.e === o.e) && (!o.ns || p.test(r.ns)) && (!q || r.cb === q || r.cb._cb === q) && (!n || r.ctx === n)
		})
	}

	function j(m, l) {
		if (!(this instanceof j)) {
			return new j(m, l)
		}
		l && f.extend(this, l);
		this.type = m;
		return this
	}
	j.prototype = {
		isDefaultPrevented: a,
		isPropagationStopped: a,
		preventDefault: function() {
			this.isDefaultPrevented = d
		},
		stopPropagation: function() {
			this.isPropagationStopped = d
		}
	};
	e.event = {
		on: function(l, p, m) {
			var n = this,
				o;
			if (!p) {
				return this
			}
			o = this._events || (this._events = []);
			k(l, p, function(q, s) {
				var r = c(q);
				r.cb = s;
				r.ctx = m;
				r.ctx2 = m || n;
				r.id = o.length;
				o.push(r)
			});
			return this
		},
		one: function(l, o, m) {
			var n = this;
			if (!o) {
				return this
			}
			k(l, o, function(p, r) {
				var q = function() {
					n.off(p, q);
					return r.apply(m || n, arguments)
				};
				q._cb = r;
				n.on(p, q, m)
			});
			return this
		},
		off: function(l, o, n) {
			var m = this._events;
			if (!m) {
				return this
			}
			if (!l && !o && !n) {
				this._events = [];
				return this
			}
			k(l, o, function(p, q) {
				b(m, p, q, n).forEach(function(r) {
					delete m[r.id]
				})
			});
			return this
		},
		trigger: function(m) {
			var q = -1,
				o, p, n, l, r;
			if (!this._events || !m) {
				return this
			}
			typeof m === "string" && (m = new j(m));
			o = i.call(arguments, 1);
			m.args = o;
			o.unshift(m);
			p = b(this._events, m.type);
			if (p) {
				l = p.length;
				while (++q < l) {
					if ((n = m.isPropagationStopped()) || false === (r = p[q]).cb.apply(r.ctx2, o)) {
						n || (m.stopPropagation(), m.preventDefault());
						break
					}
				}
			}
			return this
		}
	};
	e.Event = j
})(gmu, gmu.$);
/*!Extend widget.js*/
(function(j, f, h) {
	var m = [].slice,
		n = Object.prototype.toString,
		q = function() {}, d = ["options", "template", "tpl2html"],
		e = (function() {
			var t = {}, v = 0,
				u = "_gid";
			return function(z, y, A) {
				var w = z[u] || (z[u] = ++v),
					x = t[w] || (t[w] = {});
				A !== h && (x[y] = A);
				A === null && delete x[y];
				return x[y]
			}
		})(),
		o = j.event;

	function i(t) {
		return n.call(t) === "[object Object]"
	}

	function k(u, t) {
		u && Object.keys(u).forEach(function(v) {
			t(v, u[v])
		})
	}

	function c(u) {
		try {
			u = u === "true" ? true : u === "false" ? false : u === "null" ? null : +u + "" === u ? +u : /(?:\{[\s\S]*\}|\[[\s\S]*\])$/.test(u) ? JSON.parse(u) : u
		} catch (t) {
			u = h
		}
		return u
	}

	function g(x) {
		var v = {}, u = x && x.attributes,
			t = u && u.length,
			w, y;
		while (t--) {
			y = u[t];
			w = y.name;
			if (w.substring(0, 5) !== "data-") {
				continue
			}
			w = w.substring(5);
			y = c(y.value);
			y === h || (v[w] = y)
		}
		return v
	}

	function l(u) {
		var v = u.substring(0, 1).toLowerCase() + u.substring(1),
			t = f.fn[v];
		f.fn[v] = function(y) {
			var x = m.call(arguments, 1),
				A = typeof y === "string" && y,
				w, z;
			f.each(this, function(B, C) {
				z = e(C, u) || new j[u](C, i(y) ? y : h);
				if (A === "this") {
					w = z;
					return false
				} else {
					if (A) {
						if (!f.isFunction(z[A])) {
							throw new Error("\u7ec4\u4ef6\u6ca1\u6709\u6b64\u65b9\u6cd5\uff1a" + A)
						}
						w = z[A].apply(z, x);
						if (w !== h && w !== z) {
							return false
						}
						w = h
					}
				}
			});
			return w !== h ? w : this
		};
		f.fn[v].noConflict = function() {
			f.fn[v] = t;
			return this
		}
	}

	function a(t, v) {
		var u = this;
		if (t.superClass) {
			a.call(u, t.superClass, v)
		}
		k(e(t, "options"), function(w, x) {
			x.forEach(function(z) {
				var A = z[0],
					y = z[1];
				if (A === "*" || (f.isFunction(A) && A.call(u, v[w])) || A === v[w]) {
					y.call(u)
				}
			})
		})
	}

	function s(t, v) {
		var u = this;
		if (t.superClass) {
			s.call(u, t.superClass, v)
		}
		k(e(t, "plugins"), function(w, x) {
			if (v[w] === false) {
				return
			}
			k(x, function(y, A) {
				var z;
				if (f.isFunction(A) && (z = u[y])) {
					u[y] = function() {
						var B = u.origin,
							C;
						u.origin = z;
						C = A.apply(u, arguments);
						B === h ? delete u.origin : (u.origin = B);
						return C
					}
				} else {
					u[y] = A
				}
			});
			x._init.call(u)
		})
	}

	function p() {
		var t = m.call(arguments),
			u = t.length,
			v;
		while (u--) {
			v = v || t[u];
			i(t[u]) || t.splice(u, 1)
		}
		return t.length ? f.extend.apply(null, [true, {}].concat(t)) : v
	}

	function b(v, t, w, x, u) {
		var z = this,
			y;
		if (i(x)) {
			u = x;
			x = h
		}
		u && u.el && (x = f(u.el));
		x && (z.$el = f(x), x = z.$el[0]);
		y = z._options = p(t.options, g(x), u);
		z.template = p(t.template, y.template);
		z.tpl2html = p(t.tpl2html, y.tpl2html);
		z.widgetName = v.toLowerCase();
		z.eventNs = "." + z.widgetName + w;
		z._init(y);
		z._options.setup = (z.$el && z.$el.parent()[0]) ? true : false;
		a.call(z, t, y);
		s.call(z, t, y);
		z._create();
		z.trigger("ready");
		x && e(x, v, z) && z.on("destroy", function() {
			e(x, v, null)
		});
		return z
	}

	function r(w, v, y) {
		if (typeof y !== "function") {
			y = j.Base
		}
		var x = 1,
			u = 1;

		function t(A, z) {
			if (w === "Base") {
				throw new Error("Base\u7c7b\u4e0d\u80fd\u76f4\u63a5\u5b9e\u4f8b\u5316")
			}
			if (!(this instanceof t)) {
				return new t(A, z)
			}
			return b.call(this, w, t, x++, A, z)
		}
		f.extend(t, {
			register: function(A, B) {
				var z = e(t, "plugins") || e(t, "plugins", {});
				B._init = B._init || q;
				z[A] = B;
				return t
			},
			option: function(A, B, C) {
				var z = e(t, "options") || e(t, "options", {});
				z[A] || (z[A] = []);
				z[A].push([B, C]);
				return t
			},
			inherits: function(z) {
				return r(w + "Sub" + u++, z, t)
			},
			extend: function(B) {
				var A = t.prototype,
					z = y.prototype;
				d.forEach(function(C) {
					B[C] = p(y[C], B[C]);
					B[C] && (t[C] = B[C]);
					delete B[C]
				});
				k(B, function(C, D) {
					if (typeof D === "function" && z[C]) {
						A[C] = function() {
							var F = this.$super,
								E;
							this.$super = function() {
								var G = m.call(arguments, 1);
								return z[C].apply(this, G)
							};
							E = D.apply(this, arguments);
							F === h ? (delete this.$super) : (this.$super = F);
							return E
						}
					} else {
						A[C] = D
					}
				})
			}
		});
		t.superClass = y;
		t.prototype = Object.create(y.prototype);
		t.extend(v);
		return t
	}
	j.define = function(u, t, v) {
		j[u] = r(u, t, v);
		l(u)
	};
	j.isWidget = function(u, t) {
		t = typeof t === "string" ? j[t] || q : t;
		t = t || j.Base;
		return u instanceof t
	};
	j.Base = r("Base", {
		_init: q,
		_create: q,
		getEl: function() {
			return this.$el
		},
		on: o.on,
		one: o.one,
		off: o.off,
		trigger: function(v) {
			var t = typeof v === "string" ? new j.Event(v) : v,
				u = [t].concat(m.call(arguments, 1)),
				x = this._options[t.type],
				w = this.getEl();
			if (x && f.isFunction(x)) {
				false === x.apply(this, u) && (t.stopPropagation(), t.preventDefault())
			}
			o.trigger.apply(this, u);
			w && w.triggerHandler(t, (u.shift(), u));
			return this
		},
		tpl2html: function(v, u) {
			var t = this.template;
			t = typeof v === "string" ? t[v] : ((u = v), t);
			return u || ~t.indexOf("<%") ? f.parseTpl(t, u) : t
		},
		destroy: function() {
			this.$el && this.$el.off(this.eventNs);
			this.trigger("destroy");
			this.off();
			this.destroyed = true
		}
	}, Object);
	f.ui = j
})(gmu, gmu.$);
/*!Widget slider/slider.js*/
(function(d, c, e) {
	var f = c.fx.cssPrefix,
		a = c.fx.transitionEnd,
		b = " translateZ(0)";
	d.define("Slider", {
		options: {
			loop: false,
			speed: 400,
			index: 0,
			selector: {
				container: ".ui-slider-group"
			}
		},
		template: {
			item: '<div class="ui-slider-item"><a href="<%= href %>"><img src="<%= pic %>" alt="" /></a><% if( title ) { %><p><%= title %></p><% } %></div>'
		},
		_create: function() {
			var i = this,
				g = i.getEl(),
				h = i._options;
			i.index = h.index;
			i._initDom(g, h);
			i._initWidth(g, i.index);
			i._container.on(a + i.eventNs, c.proxy(i._tansitionEnd, i));
			c(window).on("ortchange" + i.eventNs, function() {
				i._initWidth(g, i.index)
			})
		},
		_initDom: function(j, l) {
			var g = l.selector,
				k = l.viewNum || 1,
				i, h;
			h = j.find(g.container);
			if (!h.length) {
				h = c("<div></div>");
				if (!l.content) {
					if (j.is("ul")) {
						this.$el = h.insertAfter(j);
						h = j;
						j = this.$el
					} else {
						h.append(j.children())
					}
				} else {
					this._createItems(h, l.content)
				}
				h.appendTo(j)
			}
			if ((i = h.children()).length < k + 1) {
				l.loop = false
			}
			while (l.loop && h.children().length < 3 * k) {
				h.append(i.clone())
			}
			this.length = h.children().length;
			this._items = (this._container = h).addClass("ui-slider-group").children().addClass("ui-slider-item").toArray();
			this.trigger("done.dom", j.addClass("ui-slider"), l)
		},
		_createItems: function(h, j) {
			var k = 0,
				g = j.length;
			for (; k < g; k++) {
				h.append(this.tpl2html("item", j[k]))
			}
		},
		_initWidth: function(h, g, k) {
			var j = this,
				i;
			if (!k && (i = h.width()) === j.width) {
				return
			}
			j.width = i;
			j._arrange(i, g);
			j.height = h.height();
			j.trigger("width.change")
		},
		_arrange: function(l, j) {
			var h = this._items,
				k = 0,
				m, g;
			this._slidePos = new Array(h.length);
			for (g = h.length; k < g; k++) {
				m = h[k];
				m.style.cssText += "width:" + l + "px;left:" + (k * -l) + "px;";
				m.setAttribute("data-index", k);
				this._move(k, k < j ? -l : k > j ? l : 0, 0)
			}
			this._container.css("width", l * g)
		},
		_move: function(j, l, k, i) {
			var g = this._slidePos,
				h = this._items;
			if (g[j] === l || !h[j]) {
				return
			}
			this._translate(j, l, k);
			g[j] = l;
			i && h[j].clientLeft
		},
		_translate: function(h, k, j) {
			var g = this._items[h],
				i = g && g.style;
			if (!i) {
				return false
			}
			i.cssText += f + "transition-duration:" + j + "ms;" + f + "transform: translate(" + k + "px, 0)" + b + ";"
		},
		_circle: function(i, h) {
			var g;
			h = h || this._items;
			g = h.length;
			return (i % g + g) % h.length
		},
		_tansitionEnd: function(g) {
			if (~~g.target.getAttribute("data-index") !== this.index) {
				return
			}
			this.trigger("slideend", this.index)
		},
		_slide: function(n, l, g, h, k, j) {
			var i = this,
				m;
			m = i._circle(n - g * l);
			if (!j.loop) {
				g = Math.abs(n - m) / (n - m)
			}
			this._move(m, -g * h, 0, true);
			this._move(n, h * g, k);
			this._move(m, 0, k);
			this.index = m;
			return this.trigger("slide", m, n)
		},
		slideTo: function(m, k) {
			if (this.index === m || this.index === this._circle(m)) {
				return this
			}
			var j = this._options,
				h = this.index,
				l = Math.abs(h - m),
				g = l / (h - m),
				i = this.width;
			k = k || j.speed;
			return this._slide(h, l, g, i, k, j)
		},
		prev: function() {
			if (this._options.loop || this.index > 0) {
				this.slideTo(this.index - 1)
			}
			return this
		},
		next: function() {
			if (this._options.loop || this.index + 1 < this.length) {
				this.slideTo(this.index + 1)
			}
			return this
		},
		getIndex: function() {
			return this.index
		},
		destroy: function() {
			this._container.off(this.eventNs);
			c(window).off("ortchange" + this.eventNs);
			return this.$super("destroy")
		}
	})
})(gmu, gmu.$);
/*!Widget slider/$lazyloadimg.js*/
(function(a) {
	a.Slider.template.item = '<div class="ui-slider-item"><a href="<%= href %>"><img lazyload="<%= pic %>" alt="" /></a><% if( title ) { %><p><%= title %></p><% } %></div>';
	a.Slider.register("lazyloadimg", {
		_init: function() {
			this.on("ready slide", this._loadItems)
		},
		_loadItems: function() {
			var g = this._options,
				c = g.loop,
				f = g.viewNum || 1,
				d = this.index,
				e, b;
			for (e = d - f, b = d + 2 * f; e < b; e++) {
				this.loadImage(c ? this._circle(e) : e)
			}
		},
		loadImage: function(c) {
			var d = this._items[c],
				b;
			if (!d || !(b = a.staticCall(d, "find", "img[lazyload]"), b.length)) {
				return this
			}
			b.each(function() {
				this.src = this.getAttribute("lazyload");
				this.removeAttribute("lazyload")
			})
		}
	})
})(gmu);
/*!Widget slider/$autoplay.js*/
(function(b, a) {
	a.extend(true, b.Slider, {
		options: {
			autoPlay: true,
			interval: 4000
		}
	});
	b.Slider.register("autoplay", {
		_init: function() {
			var c = this;
			c.on("slideend ready", c.resume).on("destory", c.stop);
			c.getEl().on("touchstart" + c.eventNs, a.proxy(c.stop, c)).on("touchend" + c.eventNs, a.proxy(c.resume, c))
		},
		resume: function() {
			var d = this,
				c = d._options;
			if (c.autoPlay && !d._timer) {
				d._timer = setTimeout(function() {
					d.slideTo(d.index + 1);
					d._timer = null
				}, c.interval)
			}
			return d
		},
		stop: function() {
			var c = this;
			if (c._timer) {
				clearTimeout(c._timer);
				c._timer = null
			}
			return c
		}
	})
})(gmu, gmu.$);
/*!Widget slider/$multiview.js*/
(function(b, a, c) {
	a.extend(b.Slider.options, {
		viewNum: 2,
		travelSize: 2
	});
	b.Slider.register("multiview", {
		_arrange: function(d, g) {
			var h = this._items,
				m = this._options.viewNum,
				k = g % m,
				e = 0,
				j = this.perWidth = Math.ceil(d / m),
				l, f;
			this._slidePos = new Array(h.length);
			for (f = h.length; e < f; e++) {
				l = h[e];
				l.style.cssText += "width:" + j + "px;left:" + (e * -j) + "px;";
				l.setAttribute("data-index", e);
				e % m === k && this._move(e, e < g ? -d : e > g ? d : 0, 0, Math.min(m, f - e))
			}
			this._container.css("width", j * f)
		},
		_move: function(f, l, k, e, j) {
			var d = this.perWidth,
				h = this._options,
				g = 0;
			j = j || h.viewNum;
			for (; g < j; g++) {
				this.origin(h.loop ? this._circle(f + g) : f + g, l + g * d, k, e)
			}
		},
		_slide: function(m, n, g, e, f, d, i) {
			var k = this,
				o = d.viewNum,
				j = this._items.length,
				h, l;
			d.loop || (n = Math.min(n, g > 0 ? m : j - o - m));
			l = k._circle(m - g * n);
			d.loop || (g = Math.abs(m - l) / (m - l));
			n %= j;
			if (j - n < o) {
				n = j - n;
				g = -1 * g
			}
			h = Math.max(0, o - n);
			if (!i) {
				this._move(l, -g * this.perWidth * Math.min(n, o), 0, true);
				this._move(m + h * g, h * g * this.perWidth, 0, true)
			}
			this._move(m + h * g, e * g, f);
			this._move(l, 0, f);
			this.index = l;
			return this.trigger("slide", l, m)
		},
		prev: function() {
			var e = this._options,
				d = e.travelSize;
			if (e.loop || (this.index > 0, d = Math.min(this.index, d))) {
				this.slideTo(this.index - d)
			}
			return this
		},
		next: function() {
			var f = this._options,
				e = f.travelSize,
				d = f.viewNum;
			if (f.loop || (this.index + d < this.length && (e = Math.min(this.length - 1 - this.index, e)))) {
				this.slideTo(this.index + e)
			}
			return this
		}
	})
})(gmu, gmu.$);
/*!Widget slider/$touch.js*/
(function(c, b, e) {
	var d = {
		touchstart: "_onStart",
		touchmove: "_onMove",
		touchend: "_onEnd",
		touchcancel: "_onEnd",
		click: "_onClick"
	}, h, g, f, a;
	b.extend(c.Slider.options, {
		stopPropagation: false,
		disableScroll: false
	});
	c.Slider.register("touch", {
		_init: function() {
			var j = this,
				i = j.getEl();
			j._handler = function(k) {
				j._options.stopPropagation && k.stopPropagation();
				return d[k.type] && j[d[k.type]].call(j, k)
			};
			j.on("ready", function() {
				i.on("touchstart" + j.eventNs, j._handler);
				j._container.on("click" + j.eventNs, j._handler)
			})
		},
		_onClick: function() {
			return !a
		},
		_onStart: function(m) {
			if (m.touches.length > 1) {
				return false
			}
			var l = this,
				n = m.touches[0],
				k = l._options,
				i = l.eventNs,
				j;
			g = {
				x: n.pageX,
				y: n.pageY,
				time: +new Date()
			};
			f = {};
			a = false;
			h = e;
			j = k.viewNum || 1;
			l._move(k.loop ? l._circle(l.index - j) : l.index - j, -l.width, 0, true);
			l._move(k.loop ? l._circle(l.index + j) : l.index + j, l.width, 0, true);
			l.$el.on("touchmove" + i + " touchend" + i + " touchcancel" + i, l._handler)
		},
		_onMove: function(o) {
			if (o.touches.length > 1 || o.scale && o.scale !== 1) {
				return false
			}
			var j = this._options,
				r = j.viewNum || 1,
				k = o.touches[0],
				m = this.index,
				l, n, p, q;
			j.disableScroll && o.preventDefault();
			f.x = k.pageX - g.x;
			f.y = k.pageY - g.y;
			if (typeof h === "undefined") {
				h = Math.abs(f.x) < Math.abs(f.y)
			}
			if (!h) {
				o.preventDefault();
				if (!j.loop) {
					f.x /= (!m && f.x > 0 || m === this._items.length - 1 && f.x < 0) ? (Math.abs(f.x) / this.width + 1) : 1
				}
				q = this._slidePos;
				for (l = m - r, n = m + 2 * r; l < n; l++) {
					p = j.loop ? this._circle(l) : l;
					this._translate(p, f.x + q[p], 0)
				}
				a = true
			}
		},
		_onEnd: function() {
			this.$el.off("touchmove" + this.eventNs + " touchend" + this.eventNs + " touchcancel" + this.eventNs, this._handler);
			if (!a) {
				return
			}
			var s = this,
				j = s._options,
				w = j.viewNum || 1,
				r = s.index,
				v = s._slidePos,
				o = +new Date() - g.time,
				n = Math.abs(f.x),
				k = !j.loop && (!r && f.x > 0 || r === v.length - w && f.x < 0),
				m = f.x > 0 ? 1 : -1,
				l, u, p, q, t;
			if (o < 250) {
				l = n / o;
				u = Math.min(Math.round(l * w * 1.2), w)
			} else {
				u = Math.round(n / (s.perWidth || s.width))
			} if (u && !k) {
				s._slide(r, u, m, s.width, j.speed, j, true);
				if (w > 1 && o >= 250 && Math.ceil(n / s.perWidth) !== u) {
					s.index < r ? s._move(s.index - 1, -s.perWidth, j.speed) : s._move(s.index + w, s.width, j.speed)
				}
			} else {
				for (p = r - w, q = r + 2 * w; p < q; p++) {
					t = j.loop ? s._circle(p) : p;
					s._translate(t, v[t], j.speed)
				}
			}
		}
	})
})(gmu, gmu.$);
/*!Widget slider/imgzoom.js*/
(function(a) {
	a.Slider.options.imgZoom = true;
	a.Slider.option("imgZoom", function() {
		return !!this._options.imgZoom
	}, function() {
		var e = this,
			b = e._options.imgZoom,
			c;
		b = typeof b === "string" ? b : "img";

		function d() {
			c && c.off("load" + e.eventNs, f)
		}

		function g() {
			d();
			c = e._container.find(b).on("load" + e.eventNs, f)
		}

		function f(i) {
			var h = i.target || this,
				j = Math.min(1, e.width / h.naturalWidth, e.height / h.naturalHeight);
			h.style.width = j * h.naturalWidth + "px"
		}
		e.on("ready dom.change", g);
		e.on("width.change", function() {
			c && c.each(f)
		});
		e.on("destroy", d)
	})
})(gmu);
/*!Widget slider/dots.js*/
(function(b, a, c) {
	a.extend(true, b.Slider, {
		template: {
			dots: '<p class="ui-slider-dots"><%= new Array( len + 1 ).join("<b></b>") %></p>'
		},
		options: {
			dots: true,
			selector: {
				dots: ".ui-slider-dots"
			}
		}
	});
	b.Slider.option("dots", true, function() {
		var d = function(g, f) {
			var e = this._dots;
			typeof f === "undefined" || b.staticCall(e[f % this.length], "removeClass", "ui-state-active");
			b.staticCall(e[g % this.length], "addClass", "ui-state-active")
		};
		this.on("done.dom", function(h, f, g) {
			var i = f.find(g.selector.dots);
			if (!i.length) {
				i = this.tpl2html("dots", {
					len: this.length
				});
				i = a(i).appendTo(f)
			}
			this._dots = i.children().toArray()
		});
		this.on("slide", function(f, h, g) {
			d.call(this, h, g)
		});
		this.on("ready", function() {
			d.call(this, this.index)
		})
	})
})(gmu, gmu.$);
/*!Widget slider/arrow.js*/
(function(b, a, c) {
	a.extend(true, b.Slider, {
		template: {
			prev: '<span class="ui-slider-pre"></span>',
			next: '<span class="ui-slider-next"></span>'
		},
		options: {
			arrow: true,
			select: {
				prev: ".ui-slider-pre",
				next: ".ui-slider-next"
			}
		}
	});
	b.Slider.option("arrow", true, function() {
		var e = this,
			d = ["prev", "next"];
		this.on("done.dom", function(i, g, h) {
			var f = h.selector;
			d.forEach(function(j) {
				var k = g.find(f[j]);
				k.length || g.append(k = a(e.tpl2html(j)));
				e["_" + j] = k
			})
		});
		this.on("ready", function() {
			d.forEach(function(f) {
				e["_" + f].on("tap" + e.eventNs, function() {
					e[f].call(e)
				})
			})
		});
		this.on("destroy", function() {
			e._prev.off(e.eventNs);
			e._next.off(e.eventNs)
		})
	})
})(gmu, gmu.$);
/*!Widget tabs/tabs.js*/
(function(e, d, f) {
	var b = 1,
		a = function() {
			return b++
		}, c = /^#(.+)$/;
	e.define("Tabs", {
		options: {
			active: 0,
			items: null,
			transition: "slide"
		},
		template: {
			nav: '<ul class="ui-tabs-nav"><% var item; for(var i=0, length=items.length; i<length; i++) { item=items[i]; %><li<% if(i==active){ %> class="ui-state-active"<% } %>><a href="javascript:;"><%=item.title%></a></li><% } %></ul>',
			content: '<div class="ui-viewport ui-tabs-content"><% var item; for(var i=0, length=items.length; i<length; i++) { item=items[i]; %><div<% if(item.id){ %> id="<%=item.id%>"<% } %> class="ui-tabs-panel <%=transition%><% if(i==active){ %> ui-state-active<% } %>"><%=item.content%></div><% } %></div>'
		},
		_init: function() {
			var j = this,
				i = j._options,
				h, g = d.proxy(j._eventHandler, j);
			j.on("ready", function() {
				h = j.$el;
				h.addClass("ui-tabs");
				i._nav.on("tap", g).children().highlight("ui-state-hover")
			});
			d(window).on("ortchange", g)
		},
		_create: function() {
			var h = this,
				g = h._options;
			if (h._options.setup && h.$el.children().length > 0) {
				h._prepareDom("setup", g)
			} else {
				g.setup = false;
				h.$el = h.$el || d("<div></div>");
				h._prepareDom("create", g)
			}
		},
		_prepareDom: function(j, n) {
			var m = this,
				k, o = m.$el,
				l, g, i, h;
			switch (j) {
				case "setup":
					n._nav = m._findElement("ul").first();
					if (n._nav) {
						n._content = m._findElement("div.ui-tabs-content");
						n._content = ((n._content && n._content.first()) || d("<div></div>").appendTo(o)).addClass("ui-viewport ui-tabs-content");
						l = [];
						n._nav.addClass("ui-tabs-nav").children().each(function() {
							var r = m._findElement("a", this),
								p = r ? r.attr("href") : d(this).attr("data-url"),
								s, q;
							s = c.test(p) ? RegExp.$1 : "tabs_" + a();
							(q = m._findElement("#" + s) || d('<div id="' + s + '"></div>')).addClass("ui-tabs-panel" + (n.transition ? " " + n.transition : "")).appendTo(n._content);
							l.push({
								id: s,
								href: p,
								title: r ? r.attr("href", "javascript:;").text() : d(this).text(),
								content: q
							})
						});
						n.items = l;
						n.active = Math.max(0, Math.min(l.length - 1, n.active || d(".ui-state-active", n._nav).index() || 0));
						m._getPanel().add(n._nav.children().eq(n.active)).addClass("ui-state-active");
						break
					}
				default:
					l = n.items = n.items || [];
					g = [];
					i = [];
					n.active = Math.max(0, Math.min(l.length - 1, n.active));
					d.each(l, function(p, q) {
						h = "tabs_" + a();
						g.push({
							href: q.href || "#" + h,
							title: q.title
						});
						i.push({
							content: q.content || "",
							id: h
						});
						l[p].id = h
					});
					n._nav = d(this.tpl2html("nav", {
						items: g,
						active: n.active
					})).prependTo(o);
					n._content = d(this.tpl2html("content", {
						items: i,
						active: n.active,
						transition: n.transition
					})).appendTo(o);
					n.container = n.container || (o.parent().length ? null : "body")
			}
			n.container && o.appendTo(n.container);
			m._fitToContent(m._getPanel())
		},
		_getPanel: function(g) {
			var h = this._options;
			return d("#" + h.items[g === f ? h.active : g].id)
		},
		_findElement: function(g, i) {
			var h = d(i || this.$el).find(g);
			return h.length ? h : null
		},
		_eventHandler: function(i) {
			var g, h = this._options;
			switch (i.type) {
				case "ortchange":
					this.refresh();
					break;
				default:
					if ((g = d(i.target).closest("li", h._nav.get(0))) && g.length) {
						i.preventDefault();
						this.switchTo(g.index())
					}
			}
		},
		_fitToContent: function(i) {
			var h = this._options,
				g = h._content;
			h._plus === f && (h._plus = parseFloat(g.css("border-top-width")) + parseFloat(g.css("border-bottom-width")));
			g.height(i.height() + h._plus);
			return this
		},
		switchTo: function(g) {
			var j = this,
				k = j._options,
				i = k.items,
				n, l, m, h, o;
			if (!k._buzy && k.active != (g = Math.max(0, Math.min(i.length - 1, g)))) {
				l = d.extend({}, i[g]);
				l.div = j._getPanel(g);
				l.index = g;
				m = d.extend({}, i[k.active]);
				m.div = j._getPanel();
				m.index = k.active;
				n = e.Event("beforeActivate");
				j.trigger(n, l, m);
				if (n.isDefaultPrevented()) {
					return j
				}
				k._content.children().removeClass("ui-state-active");
				l.div.addClass("ui-state-active");
				k._nav.children().removeClass("ui-state-active").eq(l.index).addClass("ui-state-active");
				if (k.transition) {
					k._buzy = true;
					o = d.fx.animationEnd + ".tabs";
					h = g > k.active ? "" : " reverse";
					k._content.addClass("ui-viewport-transitioning");
					m.div.addClass("out" + h);
					l.div.addClass("in" + h).on(o, function(p) {
						if (p.target != p.currentTarget) {
							return
						}
						l.div.off(o, arguments.callee);
						k._buzy = false;
						m.div.removeClass("out reverse");
						l.div.removeClass("in reverse");
						k._content.removeClass("ui-viewport-transitioning");
						j.trigger("animateComplete", l, m);
						j._fitToContent(l.div)
					})
				}
				k.active = g;
				j.trigger("activate", l, m);
				k.transition || j._fitToContent(l.div)
			}
			return j
		},
		refresh: function() {
			return this._fitToContent(this._getPanel())
		},
		destroy: function() {
			var h = this._options,
				g = this._eventHandler;
			h._nav.off("tap", g).children().highlight();
			h.swipe && h._content.off("swipeLeft swipeRight", g);
			if (!h.setup) {
				this.$el.remove()
			}
			return this.$super("destroy")
		}
	})
})(gmu, gmu.$);
/*!Widget tabs/$swipe.js*/
(function(c, b) {
	var i = 1000,
		e = 30,
		h = 70,
		g = 30,
		f = [],
		d = false,
		j = function(l) {
			for (var k = f.length; k--;) {
				if (c.contains(f[k], l)) {
					return true
				}
			}
			return false
		};

	function a() {
		c(document).on("touchstart.tabs", function(m) {
			var k = m.touches ? m.touches[0] : m,
				n, l;
			n = {
				x: k.clientX,
				y: k.clientY,
				time: Date.now(),
				el: c(m.target)
			};
			c(document).on("touchmove.tabs", function(q) {
				var o = q.touches ? q.touches[0] : q,
					p;
				if (!n) {
					return
				}
				l = {
					x: o.clientX,
					y: o.clientY,
					time: Date.now()
				};
				if ((p = Math.abs(n.x - l.x)) > g || p > Math.abs(n.y - l.y)) {
					j(q.target) && q.preventDefault()
				} else {
					c(document).off("touchmove.tabs touchend.tabs")
				}
			}).one("touchend.tabs", function() {
				c(document).off("touchmove.tabs");
				if (n && l) {
					if (l.time - n.time < i && Math.abs(n.x - l.x) > e && Math.abs(n.y - l.y) < h) {
						n.el.trigger(n.x > l.x ? "tabsSwipeLeft" : "tabsSwipeRight")
					}
				}
				n = l = b
			})
		})
	}
	gmu.Tabs.register("swipe", {
		_init: function() {
			var k = this._options;
			this.on("ready", function() {
				f.push(k._content.get(0));
				d = d || (a(), true);
				this.$el.on("tabsSwipeLeft tabsSwipeRight", c.proxy(this._eventHandler, this))
			})
		},
		_eventHandler: function(n) {
			var m = this._options,
				k, l;
			switch (n.type) {
				case "tabsSwipeLeft":
				case "tabsSwipeRight":
					k = m.items;
					if (n.type == "tabsSwipeLeft" && m.active < k.length - 1) {
						l = m.active + 1
					} else {
						if (n.type == "tabsSwipeRight" && m.active > 0) {
							l = m.active - 1
						}
					}
					l !== b && (n.stopPropagation(), this.switchTo(l));
					break;
				default:
					return this.origin(n)
			}
		},
		destroy: function() {
			var l = this._options,
				k;~
			(k = c.inArray(l._content.get(0), f)) && f.splice(k, 1);
			this.$el.off("tabsSwipeLeft tabsSwipeRight", this._eventHandler);
			f.length || (c(document).off("touchstart.tabs"), d = false);
			return this.origin()
		}
	})
})(Zepto);
/*!Widget tabs/$ajax.js*/
(function(d, e) {
	var c = /^#.+$/,
		b = {}, a = {
			loading: '<div class="ui-loading">Loading</div>',
			error: '<p class="ui-load-error">\u5185\u5bb9\u52a0\u8f7d\u5931\u8d25!</p>'
		};
	gmu.Tabs.register("ajax", {
		_init: function() {
			var h = this._options,
				f, g, j;
			this.on("ready", function() {
				f = h.items;
				for (g = 0, j = f.length; g < j; g++) {
					f[g].href && !c.test(f[g].href) && (f[g].isAjax = true)
				}
				this.on("activate", this._onActivate);
				f[h.active].isAjax && this.load(h.active)
			})
		},
		destroy: function() {
			this.off("activate", this._onActivate);
			this.xhr && this.xhr.abort();
			return this.origin()
		},
		_fitToContent: function(g) {
			var f = this._options;
			if (!f._fitLock) {
				return this.origin(g)
			}
		},
		_onActivate: function(f, g) {
			g.isAjax && this.load(g.index)
		},
		load: function(h, l) {
			var k = this,
				i = k._options,
				g = i.items,
				j, m, f;
			if (h < 0 || h > g.length - 1 || !(j = g[h]) || !j.isAjax || ((m = k._getPanel(h)).text() && !l && b[h])) {
				return this
			}(f = k.xhr) && setTimeout(function() {
				f.abort()
			}, 400);
			i._loadingTimer = setTimeout(function() {
				m.html(a.loading)
			}, 50);
			i._fitLock = true;
			k.xhr = d.ajax(d.extend(i.ajax || {}, {
				url: j.href,
				context: k.$el.get(0),
				beforeSend: function(p, n) {
					var o = gmu.Event("beforeLoad");
					k.trigger(o, p, n);
					if (o.isDefaultPrevented()) {
						return false
					}
				},
				success: function(n, p) {
					var o = gmu.Event("beforeRender");
					clearTimeout(i._loadingTimer);
					k.trigger(o, n, m, h, p);
					if (!o.isDefaultPrevented()) {
						m.html(n)
					}
					i._fitLock = false;
					b[h] = true;
					k.trigger("load", m);
					delete k.xhr;
					k._fitToContent(m)
				},
				error: function() {
					var n = gmu.Event("loadError");
					clearTimeout(i._loadingTimer);
					b[h] = false;
					k.trigger(n, m);
					if (!n.isDefaultPrevented()) {
						m.html(a.error)
					}
					delete k.xhr
				}
			}))
		}
	})
})(Zepto);
/*!Widget toolbar/toolbar.js*/
(function(b, a) {
	b.define("Toolbar", {
		options: {
			container: document.body,
			title: "\u6807\u9898",
			leftBtns: [],
			rightBtns: [],
			fixed: false
		},
		_init: function() {
			var e = this,
				d = e._options,
				c;
			if (!d.container) {
				d.container = document.body
			}
			e.on("ready", function() {
				c = e.$el;
				if (d.fixed) {
					var i = a('<div class="ui-toolbar-placeholder"></div>').height(c.offset().height).insertBefore(c).append(c).append(c.clone().css({
						"z-index": 1,
						position: "absolute",
						top: 0
					})),
						h = c.offset().top,
						f = function() {
							document.body.scrollTop > h ? c.css({
								position: "fixed",
								top: 0
							}) : c.css("position", "absolute")
						}, g;
					a(window).on("touchmove touchend touchcancel scroll scrollStop", f);
					a(document).on("touchend touchcancel", g = function() {
						setTimeout(function() {
							f()
						}, 200)
					});
					e.on("destroy", function() {
						a(window).off("touchmove touchend touchcancel scroll scrollStop", f);
						a(document).off("touchend touchcancel", g);
						c.insertBefore(i);
						i.remove();
						e._removeDom()
					});
					f()
				}
			});
			e.on("destroy", function() {
				e._removeDom()
			})
		},
		_create: function() {
			var g = this,
				c = g._options,
				k = g.getEl(),
				d = a(c.container),
				e = [],
				h = g.btnGroups = {
					left: [],
					right: []
				}, j = h.left;
			if (!c.setup) {
				(k && k.length > 0) ? k.appendTo(d) : (k = g.$el = a("<div>").appendTo(d))
			}
			e = k.children();
			$toolbarWrap = k.find(".ui-toolbar-wrap");
			if ($toolbarWrap.length === 0) {
				$toolbarWrap = a('<div class="ui-toolbar-wrap"></div>').appendTo(k)
			} else {
				e = $toolbarWrap.children()
			}
			e.forEach(function(l) {
				$toolbarWrap.append(l);
				/^[hH]/.test(l.tagName) ? (j = h.right, g.title = l) : j.push(l)
			});
			var i = $toolbarWrap.find(".ui-toolbar-left");
			var f = $toolbarWrap.find(".ui-toolbar-right");
			if (i.length === 0) {
				i = e.length ? a('<div class="ui-toolbar-left">').insertBefore(e[0]) : a('<div class="ui-toolbar-left">').appendTo($toolbarWrap);
				h.left.forEach(function(l) {
					a(l).addClass("ui-toolbar-button");
					i.append(l)
				});
				f = a('<div class="ui-toolbar-right">').appendTo($toolbarWrap);
				h.right.forEach(function(l) {
					a(l).addClass("ui-toolbar-button");
					f.append(l)
				})
			}
			k.addClass("ui-toolbar");
			a(g.title).length ? a(g.title).addClass("ui-toolbar-title") : a('<h1 class="ui-toolbar-title">' + c.title + "</h1>").insertAfter(i);
			g.btnContainer = {
				left: i,
				right: f
			};
			g.addBtns("left", c.leftBtns);
			g.addBtns("right", c.rightBtns)
		},
		_addBtn: function(c, d) {
			var e = this;
			a(d).appendTo(c).addClass("ui-toolbar-button")
		},
		addBtns: function(c, f) {
			var e = this,
				d = e.btnContainer[c],
				g = Object.prototype.toString;
			if (g.call(c) != "[object String]") {
				f = c;
				d = e.btnContainer.right
			}
			f.forEach(function(i, h) {
				if (i instanceof b.Base) {
					i = i.getEl()
				}
				e._addBtn(d, i)
			});
			return e
		},
		show: function() {
			var c = this;
			c.$el.show();
			c.trigger("show");
			c.isShowing = true;
			return c
		},
		hide: function() {
			var c = this;
			c.$el.hide();
			c.trigger("hide");
			c.isShowing = false;
			return c
		},
		toggle: function() {
			var c = this;
			c.isShowing === false ? c.show() : c.hide();
			return c
		},
		_removeDom: function() {
			var d = this,
				c = d.$el;
			if (d._options.setup === false) {
				c.remove()
			} else {
				c.css("position", "static").css("top", "auto")
			}
		}
	})
})(gmu, gmu.$);
/*!Widget toolbar/$position.js*/
(function(b, a) {
	b.Toolbar.register("position", {
		position: function(c) {
			this.$el.fix(c);
			return this
		}
	})
})(gmu, gmu.$);
/*!Widget panel/panel.js*/
(function(c, b, d) {
	var e = b.fx.cssPrefix,
		a = b.fx.transitionEnd;
	c.define("Panel", {
		options: {
			contentWrap: "",
			scrollMode: "follow",
			display: "push",
			position: "right",
			dismissible: true,
			swipeClose: true
		},
		_init: function() {
			var g = this,
				f = g._options;
			g.on("ready", function() {
				g.displayFn = g._setDisplay();
				g.$contentWrap.addClass("ui-panel-animate");
				g.$el.on(a, b.proxy(g._eventHandler, g)).hide();
				f.dismissible && g.$panelMask.hide().on("click", b.proxy(g._eventHandler, g));
				f.scrollMode !== "follow" && b(window).on("scrollStop", b.proxy(g._eventHandler, g));
				b(window).on("ortchange", b.proxy(g._eventHandler, g))
			})
		},
		_create: function() {
			if (this._options.setup) {
				var h = this,
					g = h._options,
					f = h.$el.addClass("ui-panel ui-panel-" + g.position);
				h.panelWidth = f.width() || 0;
				h.$contentWrap = b(g.contentWrap || f.next());
				g.dismissible && (h.$panelMask = b('<div class="ui-panel-dismiss"></div>').width(document.body.clientWidth - f.width()).appendTo("body") || null)
			} else {
				throw new Error("panel\u7ec4\u4ef6\u4e0d\u652f\u6301create\u6a21\u5f0f\uff0c\u8bf7\u4f7f\u7528setup\u6a21\u5f0f")
			}
		},
		_setDisplay: function() {
			var j = this,
				l = j.$el,
				g = j.$contentWrap,
				f = e + "transform",
				m = j._transDisplayToPos(),
				k = {}, i, h;
			b.each(["push", "overlay", "reveal"], function(n, o) {
				k[o] = function(q, r, p) {
					i = m[o].panel, h = m[o].cont;
					l.css(f, "translate3d(" + j._transDirectionToPos(r, i[q]) + "px,0,0)");
					if (!p) {
						g.css(f, "translate3d(" + j._transDirectionToPos(r, h[q]) + "px,0,0)");
						j.maskTimer = setTimeout(function() {
							j.$panelMask && j.$panelMask.css(r, l.width()).toggle(q)
						}, 400)
					}
					return j
				}
			});
			return k
		},
		_initPanelPos: function(f, g) {
			this.displayFn[f](0, g, true);
			this.$el.get(0).clientLeft;
			return this
		},
		_transDirectionToPos: function(g, f) {
			return g === "left" ? f : -f
		},
		_transDisplayToPos: function() {
			var f = this,
				g = f.panelWidth;
			return {
				push: {
					panel: [-g, 0],
					cont: [0, g]
				},
				overlay: {
					panel: [-g, 0],
					cont: [0, 0]
				},
				reveal: {
					panel: [0, 0],
					cont: [0, g]
				}
			}
		},
		_setShow: function(h, i, o) {
			var n = this,
				g = n._options,
				m = h ? "open" : "close",
				p = b.Event("before" + m),
				l = h !== n.state(),
				f = h ? "on" : "off",
				j = h ? b.proxy(n._eventHandler, n) : n._eventHandler,
				k = i || g.display,
				q = o || g.position;
			n.trigger(p, [i, o]);
			if (p.isDefaultPrevented()) {
				return n
			}
			if (l) {
				n._dealState(h, k, q);
				n.displayFn[k](n.isOpen = Number(h), q);
				g.swipeClose && n.$el[f](b.camelCase("swipe-" + q), j);
				g.display = k, g.position = q
			}
			return n
		},
		_dealState: function(g, i, l) {
			var j = this,
				f = j._options,
				m = j.$el,
				h = j.$contentWrap,
				n = "ui-panel-" + i + " ui-panel-" + l,
				k = "ui-panel-" + f.display + " ui-panel-" + f.position + " ui-panel-animate";
			if (g) {
				m.removeClass(k).addClass(n).show();
				f.scrollMode === "fix" && m.css("top", b(window).scrollTop());
				j._initPanelPos(i, l);
				if (i === "reveal") {
					h.addClass("ui-panel-contentWrap").on(a, b.proxy(j._eventHandler, j))
				} else {
					h.removeClass("ui-panel-contentWrap").off(a, b.proxy(j._eventHandler, j));
					m.addClass("ui-panel-animate")
				}
				j.$panelMask && j.$panelMask.css({
					left: "auto",
					right: "auto",
					height: document.body.clientHeight
				})
			}
			return j
		},
		_eventHandler: function(i) {
			var h = this,
				g = h._options,
				j = g.scrollMode,
				f = h.state() ? "open" : "close";
			switch (i.type) {
				case "click":
				case "swipeLeft":
				case "swipeRight":
					h.close();
					break;
				case "scrollStop":
					j === "fix" ? h.$el.css("top", b(window).scrollTop()) : h.close();
					break;
				case a:
					h.trigger(f, [g.display, g.position]);
					break;
				case "ortchange":
					h.$panelMask && h.$panelMask.css("height", document.body.clientHeight);
					j === "fix" && h.$el.css("top", b(window).scrollTop());
					break
			}
		},
		open: function(g, f) {
			return this._setShow(true, g, f)
		},
		close: function() {
			return this._setShow(false)
		},
		toggle: function(g, f) {
			return this[this.isOpen ? "close" : "open"](g, f)
		},
		state: function() {
			return !!this.isOpen
		},
		destroy: function() {
			this.$panelMask && this.$panelMask.off().remove();
			this.maskTimer && clearTimeout(this.maskTimer);
			this.$contentWrap.removeClass("ui-panel-animate");
			b(window).off("scrollStop", this._eventHandler);
			b(window).off("ortchange", this._eventHandler);
			return this.$super("destroy")
		}
	})
})(gmu, gmu.$);

(function(b, a, c) {
	b.define("Navigator", {
		options: {
			content: null,
			event: "click"
		},
		template: {
			list: "<ul>",
			item: '<li><a<% if( href ) { %> href="<%= href %>"<% } %>><%= text %></a></li>'
		},
		_create: function() {
			var i = this,
				h = i._options,
				g = i.getEl(),
				e = g.find("ul").first(),
				d = "ui-" + i.widgetName,
				j, f;
			if (!e.length && h.content) {
				e = a(i.tpl2html("list"));
				j = i.tpl2html("item");
				f = "";
				h.content.forEach(function(k) {
					k = a.extend({
						href: "",
						text: ""
					}, typeof k === "string" ? {
						text: k
					} : k);
					f += j(k)
				});
				e.append(f).appendTo(g)
			} else {
				if (g.is("ul, ol")) {
					e = g.wrap("<div>");
					g = g.parent()
				}
				if (h.index === c) {
					h.index = e.find(".ui-state-active").index();~
					h.index || (h.index = 0)
				}
			}
			i.$list = e.addClass(d + "-list");
			i.trigger("done.dom", g.addClass(d), h);
			e.highlight("ui-state-hover", "li");
			e.on(h.event + i.eventNs, "li:not(.ui-state-disable)>a", function(k) {
				i._switchTo(a(this).parent().index(), k)
			});
			i.index = -1;
			i.switchTo(h.index)
		},
		_switchTo: function(j, h) {
			if (j === this.index) {
				return
			}
			var f = this,
				g = f.$list.children(),
				d = b.Event("beforeselect", h),
				i;
			f.trigger(d, g.get(j));
			if (d.isDefaultPrevented()) {
				return
			}
			i = g.removeClass("ui-state-active").eq(j).addClass("ui-state-active");
			f.index = j;
			return f.trigger("select", j, i[0])
		},
		switchTo: function(d) {
			return this._switchTo(~~d)
		},
		unselect: function() {
			this.index = -1;
			this.$list.children().removeClass("ui-state-active")
		},
		getIndex: function() {
			return this.index
		}
	})
})(gmu, gmu.$);

(function(b, a, c) {
	b.Navigator.options.isScrollToNext = true;
	b.Navigator.option("isScrollToNext", true, function() {
		var e = this,
			d;
		e.on("select", function(k, m, i) {
			if (d === c) {
				d = e.index ? 0 : 1
			}
			var h = m > d,
				j = a(i)[h ? "next" : "prev"](),
				l = j.offset() || a(i).offset(),
				g = e.$el.offset(),
				f;
			if (h ? l.left + l.width > g.left + g.width : l.left < g.left) {
				f = e.$list.offset();
				e.$el.iScroll("scrollTo", h ? g.width - l.left + f.left - l.width : f.left - l.left, 0, 400)
			}
			d = m
		})
	})
})(gmu, gmu.$);

(function(b, a, c) {
	b.Navigator.options.iScroll = {
		hScroll: true,
		vScroll: false,
		hScrollbar: false,
		vScrollbar: false
	};
	b.Navigator.register("scrollable", {
		_init: function() {
			var e = this,
				d = e._options;
			e.on("done.dom", function() {
				e.$list.wrap('<div class="ui-scroller"></div>');
				e.trigger("init.iScroll");
				e.$el.iScroll(a.extend({}, d.iScroll))
			});
			a(window).on("ortchange" + e.eventNs, a.proxy(e.refresh, e));
			e.on("destroy", function() {
				e.$el.iScroll("destroy");
				a(window).off("ortchange" + e.eventNs)
			})
		},
		refresh: function() {
			this.trigger("refresh.iScroll").$el.iScroll("refresh")
		}
	})
})(gmu, gmu.$);