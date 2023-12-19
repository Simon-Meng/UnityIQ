# UnityIQ
Unity 面试总结
ET： 为什么全是静态类？
	C# 拓展方法：可以查拓展的条件和特点
	ET前端重点类梳理 https://blog.csdn.net/liyuping8888/article/details/127909809




UI框架，简洁高效。
技能系统框架及功能，灵活拓展。
 
	从传统拆解：数值
				1 属性
				2 配置
				3 计算公式
			行为
				1 移动，动画，模型变化
				2 材质，特效，音效
				3 召唤，相机动画，后处理
			控制流
				1 选择：条件和随机
				2 循环：循环
			实体：
				1 角色
				2 轻量角色（子弹）
				3 动态行为组件（buff）
	拆解完 进行组装

	使用基于时间轴的行为节点 

	时间轴+行为节点+控制流 = 完整行为表现

	控制流+多个时间轴行为节点 可实现复杂的行为表现


商业化
  
     时间轴
	主动，
	被动，通过事件来驱动
	buff，debuff (修改属性)

游戏核心逻辑

计算机 cpu 内存 gpu

计算机组成原理
计算机程序的构造和解释
编码的奥秘
同构编程数学

unity 游戏框架的封装设计思路 《主程手记》
游戏引擎架构 第三版




设计模式

问题？

	home页 主动弹出框设计

	GC回收机制？

	三件事可以造成垃圾回收的发生：

	请求堆内存分配的时候，剩余内存不能满足时，垃圾回收器会运行。
	垃圾回收器不时的自动运行(不同的平台频率不同)。
	垃圾回收可以手动运行。
	垃圾回收可能是一个频繁的操作。请求对内存分配，内存不足时候会触发垃圾回收器，这意味频繁的堆内存申请和释放可能导致频繁的垃圾回收


	1.新手引导与主逻辑分离
		基于行为树的新手引导设计
		https://blog.csdn.net/wubaohu1314/article/details/120401774?spm=1001.2101.3001.6650.5&utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7ERate-5-120401774-blog-120472571.235%5Ev39%5Epc_relevant_anti_t3_base&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7ERate-5-120401774-blog-120472571.235%5Ev39%5Epc_relevant_anti_t3_base&utm_relevant_index=6

	2.多人碰撞问题 ?
		Dots Job RVO2
	3.血条飘掉血字问题 ?
		游戏开发中，血条位置会因为角色位置改变而频繁进行刷新。这个过程会耗费比较多的性能，下面提出一些小的优化。
		1：血条缓存，这个是最基本的，不能因为血条出现一次重建一次，太费了。
		2：降低更新频率，可以尝试每五帧更新一次UI。(当然也可以是人物走了多少距离了再更新， 共同使用)
		3：将所有血条独立到一个Canvas下，因为一个canvas下的所有UI元素都是合在一个mesh中，如果界面中有大量静态元素会导致mesh重建消耗大的问题。
		4：画一个比屏幕更大的矩形区域（比如(screen.width+100, screen.height+100)）,超出这个区域再隐藏血条，要不然一个角色在屏幕边缘不断进出那就gg了。
		5：隐藏UI的时候不要使用setactive(false)和color.a =0.在UGUI中，将alpha设置为0，在drawcall上不会有任何变化，相当于贴了透明面片，还是会画到场景中。
		SetActive的消耗在https://blog.uwa4d.com/archives/QA_UGUI-1.html中的第三节界面切换中比较详细。
		Scale = 0,Alpha Group = 0快速隐藏UI，且不会有顶点重建，不会触发激活和经验的额外开销；
		6：尽量不要使用outline和shadow，前者增加四倍开销，后者增加一倍开销，会导致UI重建开销明显增大。
		
		moba游戏中，界面上会出现好多伤害数字，治疗数字，暴击数字，各种各样的图片+数字混合体出现，如果这些东西出现重叠的话，是不会动态合批的，下面提出点优化思路。
		1：可以将伤害治疗图片放到一个节点下，数字放到一个节点下，这样如果图片在一个图集里面会被合批，数字如果材质相同的话也会被合批。只是这种方法需要多点操作去维护动画。
			减少outline的使用，这玩意会让定点数增加4倍，可以整个shader，在shader里面画一下。
		2：使用textmeshPro，这是unity的一个插件。因为数字只有0到9嘛，就弄十张图，还有伤害治疗的图片，这样所有东西都是图的话就会自动合批啦。而且这个东西可以实现定制化的效果。
			这东西还有向量场，不过outline什么的效果很奇怪。
	4.框架底部封装机制

	5.浮点数的精度问题？
		int或者long 代替， 定点数

	6. A星，及怎么优化 ？

	7. 红点系统


空间与时间的博弈！



寻路 ：
		DFS
		BFS

		A星，及怎么优化 ？
			1 逻辑优化：
				1 期望值公式优化：当前最佳+期望最佳   ：从贪婪算法改为 当前最佳和预期最佳的动态规划 更快找到路径
				2 排序优化：从快排到最小堆？为啥
				3 缓存已经计算路径，命中后提取
			2 方案优化
				1 网格增加权重，引导A星走主路
				2 离线合并可行走区域，对大区域进行寻路，小区域为凸型，凸型内任意两点寻路不碰撞，减少格子
				3 离线计算寻路导航点，运行时提取，只需实时计算算路径
			变异优化
				1 跳点寻路 JSP
				2 离线预计算跳点
				JPS/JPS+/GB三种寻路的实现
		网格构建
			SDF的摇杆移动
			SDF距离场：有向距离场碰撞系统
			https://forum.cocos.org/t/sdf/93197
		
		nv mesh

		避障算法
			RVO
			RVO2
		空间分割
			KD Tree
			四叉树
			8叉树

		
		判断相撞：两个圆心距离，
			    AABB
			    SAT 分离轴定理：适用于凸多边形的碰撞检测算法，对于凹多边形则不适用为了提高效率，通常先使用 基于轴对齐包围矩形（AABB）的方法进行粗略的碰撞检测，然后再使用 
			    		    分离轴定理（SAT）做精细碰撞检测
			    GJK 算法
			    EPA


		算法拓展：算法思想：贪心，分治，回溯，动态规划

	AI： 
		Game AI Pro
		https://www.gamedev.net/tutorials/programming/artificial-intelligence/the-total-beginners-guide-to-game-ai-r4942/
		行为树，
			1、行为树提供大量的流程控制方法，使得状态之间的改变更加直观；

			2、整个游戏AI使用树型结构，方便查看与编辑；

			3、方便调试和代码编写；

			4、更好的封装性和模块性，让游戏逻辑更直观，开发者不会被那些复杂的连线绕晕。

			5、最重要的：行为树方便制作编辑器，可以交由策划人员使用

		状态机
		
		    状态模式暂时没有找到好的开源框架，但是状态模式不仅仅在AI方面使用，在游戏的框架中也被广泛使用，比如：UI框架，游戏主逻辑状态框架等等

	游戏玩法

		庄家式AI
			从技术上来看，这种控制概率的过程，就是加法和减法的过程。其核心是给出一个概率后，如何让它真的起到概率的作用，而且这种概率能让人很舒适的接受，比如当有30%概率赢时能否做到真正的随机值在30%的概率上徘徊，比如10局中有2-4局能中玩家可以得到想要的那个点位或者想要的牌什么的，而不是10局中0-5局能赢，这样输赢实际结果波动到了不好的体验
		可演算式AI
			时间轴贯穿了整个AI过程，人物之间的打斗，移动，释放法术，冷却时间等待，AI每次只计算一个时间节点，因为只有最近的那个时间节点是一定不会被其他节点影响的，这样既照顾到了可演算的根本原则，也照顾到了游戏的实时性，让战斗更加精彩。最后我们可以把每个计算的结果都记录下来，这样可以随时在客户端进行演示，整个过程的事件发生的时间都将准确无误呈现出来。如果觉得这样传输数据太浪费网络资源，则可以使用我前面提到的，传输随机种子，然后通过两边一致的算法进行各自演算。
		博弈式AI
			博弈AI最大的特点是搜索。通过搜索将所有下一步可能发生的，以及下几步可能发生的事情都记录在内存中，以此来确定电脑该怎么进行下一步动作，进而获得最大的效益



内存优化

	unity游戏使用的内存一共有三种：
	程序代码、托管堆（Managed Heap）以及本机堆（Native Heap）


		程序代码：引用的库。主要就是减少打包时的引用库，改一改build设置即可

		托管堆：是被Mono使用的一部分，内存托管堆用来存放类的实例（比如用new生成的列表，实例中的各种声明的变量等）
			  对象池，也可以手动地调用System.GC.Collect()来建议系统进行一次垃圾回收

			  https://docs.unity3d.com/cn/2021.1/Manual/BestPracticeUnderstandingPerformanceInUnity4-1.html
			  https://zhuanlan.zhihu.com/p/42585757

		本机堆：（Native Heap）：比如贴图，音效，关卡数据等
			，尽量减少在Hierarchy对资源的直接引用，而是使用Resource.Load的方法，在需要的时候从硬盘中读取资源，在使用后用Resource.UnloadAsset()和Resources.UnloadUnusedAssets()尽快将其卸载掉。总之，这里是一个处理时间和占用内存空间的trade 

			音频：

			图形渲染： 合批，减少draw call
					bebatches  ， saved by batching，
					tris，三角形数 ，verts 定点数  受摄像机影响照不到、
					shadow caster。减少阴影
					visible skinned meshs： 可见蒙皮网格，跟骨骼相绑定

			静态合批：必须具有Mesh 组件， 最多超过64000个顶点，超过另外一个批次，可以用代码在游戏中静态合批
			动态合批
			GPU Instancing：使用相同的材质和网格，相同的顶点布局和着色器，
			剔除遮挡
			光照优化
			烘焙光照
			图片优化 ：图片压缩级别选择，Filer Mode，aniso level大部分选择1，mip maps会把图片生成清晰度不同的图片，根据摄像机远近显示清晰和模糊，2d，ui图不需要勾选
					打图集减少draw call，代码加载图集
			
			UI优化 ：射线检测根据功能开启，避免使用把图片改为透明。
			导入模型优化：mesh compress压缩，read/writ 根据需要开启，animotaion type，
			LOD
			合并网格 ：mesh combie 插件
			动画优化：cull mode，（骨骼）updat when offscreen
			音频优化：使用。wav 支持最好，设置单声道，适当的声量，Load type，根据声音大小长度， 流式加载方式，优点就是不用等待数据加载完就能播放
			物理优化

			遮挡剔除

	1.对象池

	2.清除链表

	3.主动调用System.GC.Collect()

	4.检测不必要的堆内存代码：如字符串，LINQ和常量表达式，协程，结构体代替class，减少拆箱装箱

	5.缓存

	UI 优化：https://blog.uwa4d.com/archives/UWA_ReportModule8.html

Unity性能优化之内存篇
	应用程序的内存分为: 代码段, 数据段, 栈, 堆。
	代码段:
		用来存放代码的二进制指令与一些常量和常量字符串, 进程启动以后划分出来，把代码指令加载到代码段,一直占用内存，并且只读不可修改。
	数据段:
		用来存放代码中的静态全局变量，进程启动后，加载程序文件后,内存分配出来，并一直占用内存,直到进程结束。
	栈:
		函数在调用的过程中，局部变量，函数参数，函数调用时函数地址的跳转与返回执行下一条指令,这些都是使用栈来存储数据。随着函数调用返回，之前函数里面的局部变量,参数等使用的栈内存都会被回收

	堆:
		当我们希望用到内存时就分配，不用时释放， 内存的生命周期和函数调用无关，完全由开发者自己决定，也不像全局变量一样,运行后内存一直占用，直到进程退出，所以OS给我们提供了一个动态内存分配机制，它提供一个系统调用malloc来分配特定大小的内存，提供一个接口free来释放这块内存，把内存还给OS, OS会划一块区域出来专门用来动态内存分配，叫做堆

	https://blog.csdn.net/qq_41973169/article/details/127634040?spm=1001.2101.3001.6650.5&utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-5-127634040-blog-129847761.235%5Ev39%5Epc_relevant_anti_t3_base&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7ECTRLIST%7ERate-5-127634040-blog-129847761.235%5Ev39%5Epc_relevant_anti_t3_base&utm_relevant_index=10



事件系统



算法：倒没有问经典的排序，而是问A*寻路算法原理，如何优化 你们项目用的是什么寻路算法 NaveMesh算法原理
数据结构方面 Array、List、ArrayList、Dictionary的底层实现和区别 （当时我还反问了 你确定不是LinkList吗 面试官说别急嘛 会问的 _）

Array：
	数组分配在一块连续的数据空间上，因此分配空间时必须确定大小。空间的连续，也导致了存储效率低，插入和删除元素效率比较低
	   数组是一种线性结构，需要声明长度
   	通过下标查找时间复杂度为O（1）
   	插入删除比较复杂


ArrayList：
	ArrayList的大小是按照其中存储的数据来动态扩充与收缩的，ArrayList会把所有插入其中的数据都当作为object类型来处理。
	存在了装箱与拆箱的操作，会带来很大的性能损耗。

List<T>:
	 不会造成装箱和拆箱，通用型强但是效率不高，线程不安全
	声明：初始容量为0，在扩容时 按当前容量的 2倍进行扩充，缺点：会造成部分空间浪费，频繁扩容会造成GC压力
	add ，remove会调用arrary的removeat（index），然后array。copy拷贝造成内存压力
	foreach 会生成大量垃圾对象 会增加Enumerator实例 ，最后由gc单元回收
	sort  Array。sort会用快速排序，
	toArray，会重新new一个数组
	list的内存分配方式极为不合理，面对元素不断增加时会多次重新分配数组，导致原来的数值摒弃，造成GC压力，因此使用时提前告知list对象空间，减少扩容次数

HashTable
　　哈希表(HashTable)表示键/值对的集,存在装箱，拆箱

Dictionary<T,T>
	不会造成装箱和拆箱,扩容和List一样，remove时内存不会缩减

委托和事件
委托delegate：
	可以视为高级函数指针，他不仅指一向另一个函数还可以传递参数和获得返回值，系统会自动为委托对象生成同步和异步调用方式
	可以+= -=操作，
事件event：
	对delegate再次封装，限制用户直接操作delegate实例中变量的权限，只能通过注册和注销来增减

装箱和拆箱


Mono里子类不写Awake Start这些方法 还会调用吗 为什么 又是如何实现的
	会被调用，Unity内部运用了反射去调用这些生命周期函数，运行时通过反射将所有继承MonoBehaviour的类中的生命周期函数找出来，维护一套列表

Mask 优缺点 多DrawCall 但多个Mask可合批
Mask 会多出两个DrawCall 一个是 Mask 本身 会创建一个材质球 把自身Image 的像素标记到模板缓冲区StencilBuff 导致一个DrawCall 另一个是 在Mask 遍历完所有子节点 修改子节点的材质球参数 赋值为对应的裁剪参数 最后 会重置一下缓存StencilBuff 导致又有一个DrawCall


Mask2D 优缺点 没有额外DrawCall 但是 多个Mask2D里面的图是无法合批的
Mask2D 不依赖图片组件 本身组件不会产生额外DrawCall
但是他节点下的图片 不能跟节点外的进行合批 是分离的
多个Mask2D 之间也是不能合批的

AB包打包策略，不同的压缩格式有何区别

	大多数项目都选择中间状态，颗粒度会选择使用prefab和material两种形式打包，很少会细化到贴图和网格(除了UI上的Icon)，但也不少会将动画另外拎出来打包，因为这样就可以让动画按需加载了，或者以模块化的形式打包将某一个模块下的资源统一打包成一个资源文件，等各个项目都有所不同。太细化的打包方式也会让路径查找变得困难，但也不用太担心，因为可以我们可以想出更好地解决方案，比如自动生成代码，将资源以枚举方式生成数据结构，让查找变得更高效
	

资源管理。：https://zhuanlan.zhihu.com/p/100124513
 AssetBundle
 YooAsser：https://blog.csdn.net/luoyikun/article/details/131496141?spm=1001.2101.3001.6650.6&utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7ERate-6-131496141-blog-127909809.235%5Ev39%5Epc_relevant_anti_t3_base&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7EBlogCommendFromBaidu%7ERate-6-131496141-blog-127909809.235%5Ev39%5Epc_relevant_anti_t3_base&utm_relevant_index=11

如何实现资源管理和内存管理的 (值得细读琢磨)
	Unity资源管理（AssetBundle加载和卸载）及内存管理（内存的申请和释放）
	https://blog.csdn.net/u013774978/article/details/129847761

DrawCall和SetPassCall都是什么
UI合批原则
动批与静批 优缺点和限制
资源在不同平台下的压缩格式，如何取舍



网络协议 TCP、UDP；你们用的是什么 KCP 什么是KCP 又是如何实现KCP的

TCP 是一个工作在传输层的可靠数据传输的服务，它能确保接收端接收的网络包是无损坏、无间隔、非冗余和按序的
TCP 是面向连接的、可靠的、基于字节流的传输层通信协议
第三次握手是可以携带数据的，前两次握手是不可以携带数据的，这也是面试常问的题。

TCP 和 UDP 区别：
1. 连接   TCP 是面向连接的传输层协议，传输数据前先要建立连接。
		UDP 是不需要连接，即刻传输数据。
2. 服务对象 TCP 是一对一的两点服务，即一条连接只有两个端点。
		UDP 支持一对一、一对多、多对多的交互通信
3. 可靠性  TCP 是可靠交付数据的，数据可以无差错、不丢失、不重复、按需到达。
		UDP 是尽最大努力交付，不保证可靠交付数据。
4. 拥塞控制、流量控制
   	TCP 有拥塞控制和流量控制机制，保证数据传输的安全性。
	UDP 则没有，即使网络非常拥堵了，也不会影响 UDP 的发送速率。
5. 首部开销
	TCP 首部长度较长，会有一定的开销，首部在没有使用「选项」字段时是 20 个字节，如果使用了「选项」字段则会变长的。
	UDP 首部只有 8 个字节，并且是固定不变的，开销较小。

由于 TCP 是面向连接，能保证数据的可靠性交付，因此经常用于：
	FTP 文件传输
	HTTP / HTTPS
由于 UDP 面向无连接，它可以随时发送数据，再加上UDP本身的处理既简单又高效，因此经常用于：
	包总量较少的通信，如 DNS 、SNMP 等
	视频、音频等多媒体通信
	广播通信

二十七、粘包与拆包
产生原因：
要发送的数据小于TCP发送缓冲区的大小，TCP将多次写入缓冲区的数据一次发送出去，将会发生粘包；
接收数据端的应用层没有及时读取接收缓冲区中的数据，将发生粘包；
要发送的数据大于TCP发送缓冲区剩余空间大小，将会发生拆包；
待发送数据大于MSS（最大报文长度），TCP在传输前将进行拆包。即TCP报文长度-TCP头部长度>MSS。
解决策略：
消息定长。发送端将每个数据包封装为固定长度（不够的可以通过补0填充），这样接收端每次接收缓冲区中读取固定长度的数据就自然而然的把每个数据包拆分开来。
设置消息边界。服务端从网络流中按消息边界分离出消息内容。在包尾增加回车换行符进行分割，例如FTP协议。
将消息分为消息头和消息体，消息头中包含表示消息总长度（或者消息体长度）的字段。
更复杂的应用层协议。
————————————————



高级动画相关： 

 	Motion-Matching Locomotion Controller ;
 	Advanced Locomotion Controller; 
 	Animal Controller; 

	Dynamic-Parkour-System 
	Climb system 攀爬系统
	Riding System 骑乘系统

	GroundIK
	AimIK
	RagdollUtility布娃娃系统


https://zhuanlan.zhihu.com/p/380710676








