   M S B A M L   `   `   `  ����^  ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration  5   
   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   	   F BDataManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DataManagerDataManager 5   	      DataManager.AddData   5   	   WwfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  5   
   Gwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DataManager     5   	     � Title$  数据管理��5
   	   $	��600��6   $
��1000��6#    � 
ResizeMode$ CanMinimize=�60    � WindowStartupLocation$ CenterScreen=�6I   .��5      � .+�5   
   7   1System.Windows.Forms.Integration.WindowsFormsHost  $��	0,100,0,0q�6      Child. 5      '  !System.Windows.Forms.DataGridView  -   5        DG2��"  System.Windows.Forms.Control   Dock$
 Fill=�5   +      SelectionMode$ FullRowSelect=�67      RowHeadersVisible .  5      5      �� 5   
   ���	#FFF4F4F5$	��Top=�6%   $��0,50,0,0q�6=   
���   �6O   �� 5   
   $��职员编号:��6   $
��Left=�6    $��5,15,0,0q�6;   $	��Top=�6M   �� 5   
   -   5        $��txt_StaffNumber��5      $
��Left=�6)   $��23��6D   $��	65,17,0,0q�6P    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��100��6�   �� 5   
   $��
入库人:��6   $
��Left=�6   $��
170,15,0,0q�6:   $	��Top=�6N   �� 5   
   -   5        $��
txt_People��5      $
��Left=�6$   $��23��6?   $��
219,17,0,0q�6K   $
 Wrap=�6_   $	��Top=�6s   $	��100��6�   �� 5   
   $��拍摄时间:��6   $
��Left=�6    $��
322,15,0,0q�6;   $	��Top=�6O   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dp_OrginTime��5      $
��Left=�6*   $��
380,17,0,0q�6E   $	��Top=�6Y   $��98��6q   �� 5   
   $��-��6   $
��Left=�6   $��
476,16,0,0q�67   $	��Top=�6K     5   
   -   5        $��
dp_EndTime��5      $
��Left=�6'   $��
490,17,0,0q�6B   $	��Top=�6V   $��98��6n   �� 5   
   $��拍摄卫星:��6   $
��Left=�6    $��
594,15,0,0q�6;   $	��Top=�6O   �� 5   
   -   5        $��cmb_Staellite��5      $
��Left=�6(   $��
651,18,0,0q�6C   $	��Top=�6W   $	��100��6o   �� 5   
   $��轨道:��6   $
��Left=�6   $��
761,15,0,0q�69   $	��Top=�6M   �� 5   
   -   5        $��	cmb_Orbit��5      $
��Left=�6$   $��
795,18,0,0q�6?   $	��Top=�6S   $	��100��6k   �� 5   
   -   5        $��查询��5      $
��Left=�67   $��
909,16,0,0q�6R   $	��Top=�6f   $��75��6~   $��23��6�   �� 5   
   -	   5        $��上传��5      $��35��64   $
��Left=�6@   $��5,60,0,0q�6[   $	��Top=�6m   $��75��6�   �� 5   
   -
   5        $��btn_ModifyData��5   7   $��修改��6   $��35��6M   $
��Left=�6Y   $��
110,60,0,0q�6t   $	��Top=�6�   $��75��6�   �� 5    
   -   5        $��btn_DeleteData��5    7   $��删除��6   $��35��6M   $
��Left=�6Y   $��
215,60,0,0q�6t   $	��Top=�6�   $��75��6�   �� 5!   
   -   5        $��btn_Refresh��5!   8   $��刷新��6   $��35��6K   $
��Left=�6W   $	��Top=�6r   $��
320,60,0,0q�6�   $��75��6�   5"      5#      