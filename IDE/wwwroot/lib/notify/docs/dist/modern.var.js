var AWN=function(t){var e={};function s(n){if(e[n])return e[n].exports;var i=e[n]={i:n,l:!1,exports:{}};return t[n].call(i.exports,i,i.exports,s),i.l=!0,i.exports}return s.m=t,s.c=e,s.d=function(t,e,n){s.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:n})},s.r=function(t){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},s.t=function(t,e){if(1&e&&(t=s(t)),8&e)return t;if(4&e&&"object"==typeof t&&t&&t.__esModule)return t;var n=Object.create(null);if(s.r(n),Object.defineProperty(n,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var i in t)s.d(n,i,function(e){return t[e]}.bind(null,i));return n},s.n=function(t){var e=t&&t.__esModule?function(){return t.default}:function(){return t};return s.d(e,"a",e),e},s.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},s.p="",s(s.s=0)}([function(t,e,s){t.exports=s(1).default},function(t,e,s){"use strict";s.r(e);const n={maxNotifications:10,animationDuration:300,position:"bottom-right",labels:{tip:"Tip",info:"Info",success:"Success",warning:"Attention",alert:"Error",async:"Loading",confirm:"Confirmation required",confirmOk:"OK",confirmCancel:"Cancel"},icons:{tip:"question-circle",info:"info-circle",success:"check-circle",warning:"exclamation-circle",alert:"exclamation-triangle",async:"cog fa-spin",confirm:"exclamation-triangle",prefix:"<i class='fa fas fa-fw fa-",suffix:"'></i>",enabled:!0},replacements:{tip:null,info:null,success:null,warning:null,alert:null,async:null,"async-block":null,modal:null,confirm:null,general:{"<script>":"","<\/script>":""}},messages:{tip:"",info:"",success:"Action has been succeeded",warning:"",alert:"Action has been failed",confirm:"This action can't be undone. Continue?",async:"Please, wait...","async-block":"Loading"},formatError(t){if(t.response){if(!t.response.data)return"500 API Server Error";if(t.response.data.errors)return t.response.data.errors.map(t=>t.detail).join("<br>");if(t.response.statusText)return`${t.response.status} ${t.response.statusText}: ${t.response.data}`}return t.message?t.message:t},durations:{global:5e3,success:null,info:null,tip:null,warning:null,alert:null},minDurations:{async:1e3,"async-block":1e3}};class i{constructor(t={},e=n){Object.assign(this,this.defaultsDeep(e,t))}icon(t){return this.icons.enabled?`${this.icons.prefix}${this.icons[t]}${this.icons.suffix}`:""}label(t){return this.labels[t]}duration(t){let e=this.durations[t];return null===e?this.durations.global:e}toSecs(t){return`${t/1e3}s`}applyReplacements(t,e){if(!t)return this.messages[e]||"";for(const s of["general",e])if(this.replacements[s])for(const e in this.replacements[s])t=t.replace(e,this.replacements[s][e]);return t}override(t){return t?new i(t,this):this}defaultsDeep(t,e){let s={};for(const n in t)e.hasOwnProperty(n)?s[n]="object"==typeof t[n]&&null!==t[n]?this.defaultsDeep(t[n],e[n]):e[n]:s[n]=t[n];return s}}const r={popup:"awn-popup",toast:"awn-toast",btn:"awn-btn",confirm:"awn-confirm"},a={prefix:r.toast,klass:{label:`${r.toast}-label`,content:`${r.toast}-content`,icon:`${r.toast}-icon`,progressBar:`${r.toast}-progress-bar`,progressBarPause:`${r.toast}-progress-bar-paused`},ids:{container:`${r.toast}-container`}},o={prefix:r.popup,klass:{buttons:"awn-buttons",button:r.btn,successBtn:`${r.btn}-success`,cancelBtn:`${r.btn}-cancel`,title:`${r.popup}-title`,body:`${r.popup}-body`,content:`${r.popup}-content`,dotAnimation:`${r.popup}-loading-dots`},ids:{wrapper:`${r.popup}-wrapper`,confirmOk:`${r.confirm}-ok`,confirmCancel:`${r.confirm}-cancel`}},l={klass:{hiding:"awn-hiding"},lib:"awn"};var c=class{constructor(t,e,s,n,i){this.newNode=document.createElement("div"),e&&(this.newNode.id=e),s&&(this.newNode.className=s),n&&(this.newNode.style.cssText=n),this.parent=t,this.options=i}beforeInsert(){}afterInsert(){}insert(){return this.beforeInsert(),this.el=this.parent.appendChild(this.newNode),this.afterInsert(),this}replace(t){if(this.getElement())return this.beforeDelete().then(()=>(this.updateType(t.type),this.parent.replaceChild(t.newNode,this.el),this.el=this.getElement(t.newNode),this.afterInsert(),this))}beforeDelete(t=this.el){let e=0;return this.start&&(e=this.options.minDurations[this.type]+this.start-Date.now())<0&&(e=0),new Promise(s=>{setTimeout(()=>{t.classList.add(l.klass.hiding),setTimeout(s,this.options.animationDuration)},e)})}delete(t=this.el){return this.getElement(t)?this.beforeDelete(t).then(()=>{t.remove(),this.afterDelete()}):null}afterDelete(){}getElement(t=this.el){return t?document.getElementById(t.id):null}addEvent(t,e){this.el.addEventListener(t,e)}toggleClass(t){this.el.classList.toggle(t)}updateType(t){this.type=t,this.duration=this.options.duration(this.type)}},d=class extends c{constructor(t,e,s,n){super(n,`${a.prefix}-${Math.floor(Date.now()-100*Math.random())}`,`${a.prefix} ${a.prefix}-${e}`,`animation-duration: ${s.toSecs(s.animationDuration)};`,s),this.updateType(e),this.setInnerHtml(t)}setInnerHtml(t){"alert"===this.type&&t&&(t=this.options.formatError(t)),t=this.options.applyReplacements(t,this.type),this.newNode.innerHTML=`<div class="awn-toast-wrapper">${this.progressBar}${this.label}<div class="${a.klass.content}">${t}</div><span class="${a.klass.icon}">${this.options.icon(this.type)}</span></div>`}beforeInsert(){if(this.parent.childElementCount>=this.options.maxNotifications){let t=Array.from(this.parent.getElementsByClassName(a.prefix));this.delete(t.find(t=>!this.isDeleted(t)))}}afterInsert(){if("async"==this.type)return this.start=Date.now();if(this.addEvent("click",()=>this.delete()),!(this.duration<=0)){this.timer=new class{constructor(t,e){this.callback=t,this.remaining=e,this.resume()}pause(){this.paused=!0,window.clearTimeout(this.timerId),this.remaining-=new Date-this.start}resume(){this.paused=!1,this.start=new Date,window.clearTimeout(this.timerId),this.timerId=window.setTimeout(()=>{window.clearTimeout(this.timerId),this.callback()},this.remaining)}toggle(){this.paused?this.resume():this.pause()}}(()=>this.delete(),this.duration);for(const t of["mouseenter","mouseleave"])this.addEvent(t,()=>{this.isDeleted()||(this.toggleClass(a.klass.progressBarPause),this.timer.toggle())})}}isDeleted(t=this.el){return t.classList.contains(l.klass.hiding)}get progressBar(){return this.duration<=0||"async"===this.type?"":`<div class='${a.klass.progressBar}' style="animation-duration:${this.options.toSecs(this.duration)};"></div>`}get label(){return`<b class="${a.klass.label}">${this.options.label(this.type)}</b>`}},u=class extends c{constructor(t,e="modal",s,n,i){let r=`animation-duration: ${s.toSecs(s.animationDuration)};`;super(document.body,o.ids.wrapper,null,r,s),this[o.ids.confirmOk]=n,this[o.ids.confirmCancel]=i,this.className=`${o.prefix}-${e}`,["confirm","async-block","modal"].includes(e)||(e="modal"),this.updateType(e),this.setInnerHtml(t),this.insert()}setInnerHtml(t){let e=this.options.applyReplacements(t,this.type);switch(this.type){case"confirm":let t=[`<button class='${o.klass.button} ${o.klass.successBtn}'id='${o.ids.confirmOk}'>${this.options.labels.confirmOk}</button>`];!1!==this[o.ids.confirmCancel]&&t.push(`<button class='${o.klass.button} ${o.klass.cancelBtn}'id='${o.ids.confirmCancel}'>${this.options.labels.confirmCancel}</button>`),e=`${this.options.icon(this.type)}<div class='${o.klass.title}'>${this.options.label(this.type)}</div><div class="${o.klass.content}">${e}</div><div class='${o.klass.buttons} ${o.klass.buttons}-${t.length}'>${t.join("")}</div>`;break;case"async-block":e=`${e}<div class="${o.klass.dotAnimation}"></div>`}this.newNode.innerHTML=`<div class="${o.klass.body} ${this.className}">${e}</div>`}keyupListener(t){if("async-block"===this.type)return t.preventDefault();switch(t.code){case"Escape":t.preventDefault(),this.delete();case"Tab":if(t.preventDefault(),"confirm"!==this.type||!1===this[o.ids.confirmCancel])return!0;let e=this.okBtn;t.shiftKey?document.activeElement.id==o.ids.confirmOk&&(e=this.cancelBtn):document.activeElement.id!==o.ids.confirmCancel&&(e=this.cancelBtn),e.focus()}}afterInsert(){switch(this.listener=t=>this.keyupListener(t),window.addEventListener("keydown",this.listener),this.type){case"async-block":this.start=Date.now();break;case"confirm":this.okBtn.focus(),this.addEvent("click",t=>{if("BUTTON"!==t.target.nodeName)return!1;this.delete(),this[t.target.id]&&this[t.target.id]()});break;default:document.activeElement.blur(),this.addEvent("click",t=>{t.target.id===this.newNode.id&&this.delete()})}}afterDelete(){window.removeEventListener("keydown",this.listener)}get okBtn(){return document.getElementById(o.ids.confirmOk)}get cancelBtn(){return document.getElementById(o.ids.confirmCancel)}};s.d(e,"default",(function(){return h}));class h{constructor(t={}){this.options=new i(t)}tip(t,e){return this._addToast(t,"tip",e).el}info(t,e){return this._addToast(t,"info",e).el}success(t,e){return this._addToast(t,"success",e).el}warning(t,e){return this._addToast(t,"warning",e).el}alert(t,e){return this._addToast(t,"alert",e).el}async(t,e,s,n,i){let r=this._addToast(n,"async",i);return this._afterAsync(t,e,s,i,r)}confirm(t,e,s,n){return this._addPopup(t,"confirm",n,e,s)}asyncBlock(t,e,s,n,i){let r=this._addPopup(n,"async-block",i);return this._afterAsync(t,e,s,i,r)}modal(t,e,s){return this._addPopup(t,e,s)}closeToasts(){let t=this.container;for(;t.firstChild;)t.removeChild(t.firstChild)}_addPopup(t,e,s,n,i){return new u(t,e,this.options.override(s),n,i)}_addToast(t,e,s,n){s=this.options.override(s);let i=new d(t,e,s,this.container);if(n){if(n instanceof u)return n.delete().then(()=>i.insert());return n.replace(i)}return i.insert()}_afterAsync(t,e,s,n,i){return t.then(this._responseHandler(e,"success",n,i),this._responseHandler(s,"alert",n,i))}_responseHandler(t,e,s,n){return i=>{switch(typeof t){case"undefined":case"string":let r="alert"===e?t||i:t;this._addToast(r,e,s,n);break;default:n.delete().then(()=>{t&&t(i)})}}}_createContainer(){return new c(document.body,a.ids.container,`awn-${this.options.position}`).insert().el}get container(){return document.getElementById(a.ids.container)||this._createContainer()}}}]);