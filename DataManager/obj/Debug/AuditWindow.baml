   M S B A M L   `   `   `  ����^  ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration System.Windows.Forms.Integration  5   	   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   	   F BDataManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DataManagerDataManager 5   	      DataManager.AuditWindow   5   	   WwfiOclr-namespace:System.Windows.Forms.Integration;assembly=WindowsFormsIntegration  5   	   Gwf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DataManager     5   	     � Title$  审核��5
   	   $	��600��6   $
��1000��6!    � 
ResizeMode$ CanMinimize=�6.   .��5      � .+�5   
   7   1System.Windows.Forms.Integration.WindowsFormsHost  $��0��6   $��	0,100,0,0q�6+      Child. 5      '  !System.Windows.Forms.DataGridView  -   5        DG3��"  System.Windows.Forms.Control   Dock$
 Fill=�5   +      SelectionMode$ FullRowSelect=�67      RowHeadersVisible .  6o   5      �� 5   
   ���	#FFF4F4F5$	��Top=�6%   $��0,50,0,0q�6=   $��1��6O   
���   �6Z   �� 5   
   -   5        $��txt_StaffNumber��5   -   $
��Left=�6   $��25��6D   $��	70,11,0,0q�6P    �� TextWrapping$
 Wrap=�6c   $	��Top=�6x   $	��120��6�   �� 5   
   $��职员编号：��6   $
��Left=�6    $��	10,10,0,0q�6;   $	��Top=�6N   �� 5   
   $��申请人：��6   $
��Left=�6   $��
210,10,0,0q�6:   $	��Top=�6N   �� 5   
   -   5        $��
txt_People��5   -   $
��Left=�6   $��25��6?   $��
258,11,0,0q�6K   $
 Wrap=�6_   $	��Top=�6t   $	��120��6�   �� 5   
   $��审核意见：��6   $
��Left=�6    $��
402,10,0,0q�6;   $	��Top=�6O   �� 5   
   -   5        $��cmb_Opinion��5      $
��Left=�6&   $��
462,11,0,0q�6A   $��25��6U   $	��Top=�6a   $	��120��6y   �� 5   
   -   5        $��
btn_Select��5      $��查询��6   $
��Left=�6J   $��25��6e   $��
900,11,0,0q�6q   $	��Top=�6�   $��75��6�   �� 5   
   -   5        $��同意��5      $
��Left=�66   $��25��6Q   $��	10,61,0,0q�6]   $	��Top=�6p   $��75��6�   �� 5   
   -   5        $��拒绝��5      $
��Left=�69   $��25��6T   $��
120,61,0,0q�6`   $	��Top=�6t   $��75��6�   �� 5   
   -   5        $��刷新��5      $
��Left=�68   $��25��6S   $��
235,61,0,0q�6_   $	��Top=�6s   $��75��6�   �� 5   
   -	   5         ��Name$ 	b_Opinion��5   %   
���   �
���  ��$��1q�6I   $
��Left=�6]   $��15��6x   $��
235,61,0,0q�6�   $	��Top=�6�   $��15��6�   $y�	Collapsed=�6�   ._�5      �� -
   5        $��lb_ApplicationNumber��5       $��9��6   $��-5,-3,-8,-8q�6<   $��8(�6Q   $y�	Collapsed=�6^   5      �� 5   
   $��申请时间:��6   $
��Left=�6    $��
608,10,0,0q�6;   $	��Top=�6O   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��dp_OrginTime��5   0   $
��Left=�6   $��
665,11,0,0q�6D   $	��Top=�6X   �� 5   
   $��-��6   $
��Left=�6   $��
745,10,0,0q�67   $	��Top=�6K     5    
   -   5        $��
dp_EndTime��5    0   $
��Left=�6   $��
760,11,0,0q�6B   $	��Top=�6V   5!      5"      