<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html"/>
	<xsl:template match="/">
		<html>
			<body>
				<table border="2">
					<TR>
						<th>Name</th>
						<th>Genre</th>
						<th>Year</th>
						<th>Director</th>
						<th>Country</th>
						<th>Language</th>
					</TR>
					<xsl:for-each select="Films/Film">
						<tr>
							<td>
								<xsl:value-of select="@Name">
								</xsl:value-of>
							</td>
							<td>
								<xsl:value-of select="@Genre">
								</xsl:value-of>
							</td>
							<td>
								<xsl:value-of select="@Year">
								</xsl:value-of>
							</td>
							<td>
								<xsl:value-of select="@Director">
								</xsl:value-of>
							</td>
							<td>
								<xsl:value-of select="@Country">
								</xsl:value-of>
							</td>
							<td>
								<xsl:value-of select="@Language">
								</xsl:value-of>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>