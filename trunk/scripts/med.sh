CAT=$1
GENNAME=$2
COL=$3

temp=/tmp/$0-$$
temp2=/tmp/$0-$$-2
touch $temp
input=sourcesandprices.txt


case $# in
	0)	#No args - print all categories
		grep '^P[[:digit:]]\{2\} [[:upper:]]' $input
		;;
	1)	#1 arg - print all gennames from a specific category
		if [ "$CAT" == "*" ]; then
		        grep '([[:digit:]]*)' $input | sed 's/([[:digit:]]*)//' | sed 's/^[ \t]*//;s/[ \t]*$//'
		#else
		        #;grep '^P[[:digit:]]\{2\} [[:upper:]]' $input
		fi

		;; 

	2|3)	#2 args - print all presentations from a specific gennames
		if [ "$GENNAME" == "*" ]; then
		        grep -v -e '([[:digit:]]*)' -e '^P[[:digit:]]\{2\}' -e '^[[:space:]]*$' -e '^PRODUCT' -e '---' $input > $temp

#| sed 's/([[:digit:]]*)//' | sed 's/^[ \t]*//;s/[ \t]*$//' >> $temp
		else
		       	 sed -n "/$GENNAME/,/^[[:space:]]*\$/p" $input > $temp
		fi
		;;
	3)	#3 args - print a column for a presentation
		#sed -n "/$GENNAME/,/^[[:space:]]*\$/p" $temp | awk -F '\t+' "{print \$$COL}"
		echo "3 $COL"
		awk -F '\t+' "{print \$$COL}" $temp #> $temp2
#		cp -v $temp2 $temp
		;;
esac

cat $temp;
