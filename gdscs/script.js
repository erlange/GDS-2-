function pre()
{
	FP_preloadImgs(
		'images/buttonC.jpg'
		, 'images/buttonD.jpg'
		, 'images/buttonF.jpg'
		, 'images/button10.jpg'

		, 'images/y0.gif'
		, 'images/y0l.gif'
		, 'images/y0r.gif'
		, 'images/y1l.gif'
		, 'images/y1r.gif'
		, 'images/y2l.gif'
		, 'images/y2r.gif'
		, 'images/y3l.gif'
		, 'images/y3r.gif'
		, 'images/y4l.gif'
		, 'images/y4r.gif'
		, 'images/y5r.gif'
		, 'images/y5l.gif'
		, 'images/yyl.gif'
		, 'images/yyr.gif'
		, 'images/yyy.gif'
		
		, 'images/z0.gif'
		, 'images/z0l.gif'
		, 'images/z0r.gif'
		, 'images/z1l.gif'
		, 'images/z1r.gif'
		, 'images/z2l.gif'
		, 'images/z2r.gif'
		, 'images/z3l.gif'
		, 'images/z3r.gif'
		, 'images/z4l.gif'
		, 'images/z4r.gif'
		, 'images/z5r.gif'
		, 'images/z5l.gif'
		, 'images/zzl.gif'
		, 'images/zzr.gif'
		, 'images/zzz.gif'
		)	
}

function FP_preloadImgs() {//v1.0
 var d=document,a=arguments; if(!d.FP_imgs) d.FP_imgs=new Array();
 for(var i=0; i<a.length; i++) { d.FP_imgs[i]=new Image; d.FP_imgs[i].src=a[i]; }
}

function FP_swapImg() {//v1.0
 var doc=document,args=arguments,elm,n; doc.$imgSwaps=new Array(); for(n=2; n<args.length;
 n+=2) { elm=FP_getObjectByID(args[n]); if(elm) { doc.$imgSwaps[doc.$imgSwaps.length]=elm;
 elm.$src=elm.src; elm.src=args[n+1]; } }
}

function FP_getObjectByID(id,o) {//v1.0
 var c,el,els,f,m,n; if(!o)o=document; if(o.getElementById) el=o.getElementById(id);
 else if(o.layers) c=o.layers; else if(o.all) el=o.all[id]; if(el) return el;
 if(o.id==id || o.name==id) return o; if(o.childNodes) c=o.childNodes; if(c)
 for(n=0; n<c.length; n++) { el=FP_getObjectByID(id,c[n]); if(el) return el; }
 f=o.forms; if(f) for(n=0; n<f.length; n++) { els=f[n].elements;
 for(m=0; m<els.length; m++){ el=FP_getObjectByID(id,els[n]); if(el) return el; } }
 return null;
}

function mn(w,bc,c)
{
	w.style.backgroundColor=bc;
	w.style.color=c;
}
function adv(n)
{
	var f;
	f=document.forms[n]
	f.ds.value=f.lstds2.value;
	return true;
}
function chs(w,n)
{
	w.className=n;
}
function mz(f,c,ln)
{
	var el = document.forms[f];
	var iCnt = 0;
	for (var i=0;i<el.v.length;i++)
	{
		if (el.v[i].checked)
		{
			iCnt ++;
		}
	}
	if(c==1)
	{
	    if(iCnt!=2)
			{
   				alert('Anda harus memilih dua pilihan dari daftar kuesioner. \n\nYou must select exactly 2 variables.');
				return false;
		    }
	}
	else
	{
	    if(iCnt!=1)
	    {
   			alert('Anda harus memilih satu pilihan dari daftar kuesioner. \n\nYou must select one variable from the list.');
			return false;
	    }
	}
	
}
function dist(f,l)
{
	var sArr='';
	for (var i=0;i<f.length;i++)
	{
		if (el.l[i].checked)
		{
			sArr=el.l[i].value;
		}
	}
	if (sArr=='')
	{
        alert('Anda belum memilih daerah (propinsi atau kabupaten). \n\nYou have not selected a province or region.');
        return false;
	}
	else
	{
        el.d.value=oArr[1];
        el.r.value=oArr[0];
        return true;
    	//el.submit();
	}

}
function dist3(f,l)
{
	var sArr='';
	var el = document.forms[f];
	var iCnt = 0;var sArr;
	for (var i=0;i<el.l.length;i++)
	{
		if (el.l[i].checked)
		{
			sArr=el.l[i].value;
		}
	}
   	var oArr=sArr.split('~');
	if (sArr=='')
	{
        alert('Anda belum memilih daerah (propinsi atau kabupaten). \n\nYou have not selected a province or region.');
        return false;
	}
	else
	{
        el.d.value=oArr[1];
        el.r.value=oArr[0];
        return true;
    	//el.submit();
	}
}
function sh0(id)
{
	if (document.all[id].style.display=='none')
	{
		document.all[id].style.display='block';
		document.all[id + 'img'].src='images/minus.gif';
	}
	else
	{
		document.all[id].style.display='none';
		document.all[id + 'img'].src='images/plus.gif';
	}
}

function sh(id)
{
	if (document.all[id].style.display=='none')
	{
		document.all[id].style.display='block';
		document.all[id + 'img'].src='images/fmin.gif';
	}
	else
	{
		document.all[id].style.display='none';
		document.all[id + 'img'].src='images/fplus.gif';
	}
}
function tb()
{
	var el = window.event.srcElement;
	if (el.className=='imgToolbar')
		{el.className='imgToolbarHover'}
	else if(el.className=='imgToolbarHover')
		{el.className='imgToolbar'}
}
function ca()
{
	switch (window.event.srcElement.className)
	{
		case ('i0'):
			window.event.srcElement.className='i0O';break;
		case ('i0O'):
			window.event.srcElement.className='i0';break;
		case ('i1'):
			window.event.srcElement.className='i1O';break;
		case ('i1O'):
			window.event.srcElement.className='i1';break;
		case ('i2'):
			window.event.srcElement.className='i2O';break;
		case ('i2O'):
			window.event.srcElement.className='i2';break;
		case ('i3'):
			window.event.srcElement.className='i3O';break;
		case ('i3O'):
			window.event.srcElement.className='i3';break;
		case ('iFl'):
			window.event.srcElement.className='iFlO';break;
		case ('iFlO'):
			window.event.srcElement.className='iFl';break;
		case ('i4'):
			window.event.cancelBubble=true;break;
		case ('i4O'):
			window.event.cancelBubble=true;break;
	}
}
function ch(id)
{
	document.all[id].style.visibility='hidden';
}
function ci(id,t)
{
	if (document.all[id].style.visibility=='hidden')
	{
		document.all[id].style.visibility='visible';
	}
	else
	{
		document.all[id].style.visibility='hidden';
	}
	document.all[id].innerHTML=t;
}
function cm(id)
{
	document.all[id].style.pixelLeft = document.body.scrollLeft+event.clientX;
	document.all[id].style.pixelTop = document.body.scrollTop+event.clientY;
}
function ciD(id,d)
{
	if (document.all[id].style.visibility=='hidden')
	{
		document.all[id].style.visibility='visible';
	}
	else
	{
		document.all[id].style.visibility='hidden';
	}
	document.all[id].innerHTML='Propinsi : ' + d ;
}

function cmm(e)
{
	var el = document.forms['f1'];
	var iCnt = 0;var s;
	s=e + el.ds.options[el.ds.selectedIndex].value;
	document.location.href=s;
}

function cmmz(e,ds)
{
	var iCnt = 0;var s;
	s=e + ds.options[ds.selectedIndex].value;
	document.location.href=s;
}

function fnSave()
{
	var sArr="";
	var iCnt = 0;
	for(var i=0;i<document.all.length;i++)
	{
		oTrap=document.all[i];
		if ((oTrap.id.indexOf("d")==0)&&(oTrap.tagName=="DIV"))
		{
			if(sArr.length==0)
			{
				sArr=oTrap.style.display;
			}
			else sArr+="|" + oTrap.style.display;
		}
	}
	document.all['TOC'].setAttribute("z",sArr);
}

function fnLoad()
{
	var sArr=document.all['TOC'].getAttribute("z");
	var cyc=0;
	var oArr=sArr.split("|");
	for (var i=0;i<document.all.length;i++)
	{
		oTrap=document.all[i];
		if ((oTrap.id.indexOf("d")==0)&&(oTrap.tagName=="DIV"))
		{
   			oTrap.style.display=oArr[cyc];
			cyc++;
		}
	}
}
var err=0;
function vl(n)
{
	var m=eval('document.frmVar.' + n + '.options[document.frmVar.' + n + '.selectedIndex].value');
	if(m==0)
	{
		alert('Pilihan Anda tidak sah.\n\n The selection is not valid.');
		if (document.getElementById)
		{
			document.all('err' + n).innerHTML="&lt;-Error ";
			document.all('err' + n).title= 'Pilihan harus yang level terdalam. The value must be the deepest level of hierarchy';
		}
		err++;
	}
	else
	{
		if (document.getElementById)
		{
			document.all('err' + n).innerHTML="";
			document.all('err' + n).title='';
		}
		err--;
	}
}
function vt(n)
{
	var z=eval('document.frmVar.' + n + '.value');
	if ((isNaN(z))||(z>10))
	{
		alert('Harus numerik dan tidak boleh melebihi 10. \n\nMust be a number and less than or equals 10.');
		if (document.getElementById)
		{
			document.all('err' + n).innerHTML="&lt;-Error ";
			document.all('err' + n).title='Harus numerik dan kurang dari sepuluh. The value must numeric and less than 10';
		}
		err++;
	}
	else
	{
		if (document.getElementById)
		{
			document.all('err' + n).innerHTML="";
			document.all('err' + n).title='';
		}
		err--;
	}
	
	var vt0=parseFloat(document.frmVar.vt0.value); 
	var vt1=parseFloat(document.frmVar.vt1.value); 
	var vt2=parseFloat(document.frmVar.vt2.value); 
	var vt3=parseFloat(document.frmVar.vt3.value); 
	var vt4=parseFloat(document.frmVar.vt4.value); 
	document.frmVar.vt10.value= vt0+vt1+vt2+vt3+vt4;
}
